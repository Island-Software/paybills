using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class BillRepository : RepositoryBase, IBillRepository
    {
        public BillRepository(DataContext context) : base(context) { }

        public void Create(Bill bill) => _context.Bills.Add(bill);

        public void Delete(Bill bill) => _context.Bills.Remove(bill);

        public async Task<PagedList<Bill>> GetBillsAsync(string username, UserParams userParams)
        {
            var query =  _context.Bills
                .Include(b => b.BillType)
                .Include(b => b.Users)
                .Where(b => b.Users.Count(u => u.UserName == username) > 0)
                .OrderBy(b => b.Id)                
                .AsNoTracking();     

            return await PagedList<Bill>.CreateAsync(query, userParams.PageNumber, userParams.PageSize);
        } 

        public async Task<List<Bill>> GetBillsAsync(int userId)
        {
            var bills =  _context.Bills
                .Include(b => b.BillType)
                .Where(b => b.Users.Count(u => u.Id == userId) > 0)
                .OrderBy(b => b.Id)                
                .AsNoTracking();

            return await bills.ToListAsync();
        }

        private async Task<List<Bill>> GetBillsAsync(int userId, int currentMonth, int currentYear)
        {
            var bills =  _context.Bills
                .Include(b => b.BillType)
                .Where(b => b.Users.Count(u => u.Id == userId) > 0 && b.Month == currentMonth && b.Year == currentYear)
                .OrderBy(b => b.Id)                
                .AsNoTracking();

            return await bills.ToListAsync();
        }

        public async Task<PagedList<Bill>> GetBillsByDateAsync(string username, int month, int year, UserParams userParams)
        {
            var query =  _context.Bills
                .Include(b => b.BillType)
                .Include(b => b.Users)
                .Where(b => b.Users.Count(u => u.UserName == username) > 0 && b.Month == month && b.Year == year)
                .OrderBy(b => b.Id)                
                .AsNoTracking();     

            return await PagedList<Bill>.CreateAsync(query, userParams.PageNumber, userParams.PageSize);
        }

        public async Task<Bill> GetBillByIdAsync(int id) => await _context.Bills.Include(b => b.BillType).SingleAsync(b => b.Id == id);

        public void Update(Bill bill) => _context.Entry(bill).State = EntityState.Modified;

        public async Task<bool> AddBillToUser(int userId, int billId)
        {
            var user = await _context.Users
                .Include(u => u.Bills)
                .SingleAsync(u => u.Id == userId);

            var bill = await _context.Bills
                .SingleAsync(b => b.Id == billId);

            user.Bills.Add(bill);

            return true;
        }

        public async Task<bool> AddBillsToUser(int userId, IEnumerable<Bill> bills)
        {
            var user = await _context.Users
                .Include(u => u.Bills)
                .SingleAsync(u => u.Id == userId);

            foreach (var bill in bills)
            {
                user.Bills.Add(bill);
            }            

            return true;
        }

        public async Task<bool> CopyBillsToNextMonth(int userId, int currentMonth, int currentYear)
        {
            var bills = await GetBillsAsync(userId, currentMonth, currentYear);

            foreach (var bill in bills)
            {
                _context.Entry(bill.BillType).State = EntityState.Unchanged;

                if (bill.Month == 12)
                {
                    bill.Month = 1;
                    bill.Year += 1;
                }
                else
                {
                    bill.Month += 1;
                }              

                bill.Id = 0;
                bill.Value = 0;
                Create(bill);                  

                await _context.SaveChangesAsync();                         
            }   
            await AddBillsToUser(userId, bills);

            return true;
        }
    }
}