using System.Collections.Generic;
using System.Threading.Tasks;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class BillTypeRepository : IBillTypeRepository
    {
        private readonly DataContext _context;

        public BillTypeRepository(DataContext context)
        {
            this._context = context;
        }

        public void Create(BillType billType)
        {
            _context.BillTypes.Add(billType);
        }

        public void Delete(BillType billType)
        {
            _context.BillTypes.Remove(billType);
        }

        public async Task<BillType> GetBillTypeByIdAsync(int id)
        {
            return await _context.BillTypes.SingleAsync(bt => bt.Id == id);
        }

        public async Task<IEnumerable<BillType>> GetBillTypesAsync()
        {
            return await _context.BillTypes.ToListAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(BillType billType)
        {
            _context.Entry(billType).State = EntityState.Modified;
        }
    }
}