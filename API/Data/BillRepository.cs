using System.Collections.Generic;
using System.Threading.Tasks;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class BillRepository : IBillRepository
    {
        private readonly DataContext _context;

        public BillRepository(DataContext context)
        {
            this._context = context;
        }

        public void Create(Bill bill)
        {
            _context.Bills.Add(bill);
        }

        public void Delete(Bill bill)
        {
            _context.Bills.Remove(bill);
        }

        public async Task<Bill> GetBillByIdAsync(int id)
        {
            return await _context.Bills.Include(b => b.BillType).SingleAsync(b => b.Id == id);
        }

        public async Task<IEnumerable<Bill>> GetBillsAsync()
        {
            return await _context.Bills.Include(b => b.BillType).ToListAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(Bill bill)
        {
            _context.Entry(bill).State = EntityState.Modified;
        }
    }
}