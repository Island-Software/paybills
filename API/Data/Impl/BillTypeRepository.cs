using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class BillTypeRepository : RepositoryBase, IBillTypeRepository
    {
        public BillTypeRepository(DataContext context) : base(context) { }

        public async Task<bool> BillTypeExists(string description) => await _context.BillTypes.CountAsync(bt => bt.Description == description) > 0;

        public void Create(BillType billType) => _context.BillTypes.Add(billType);

        public void Delete(BillType billType) => _context.BillTypes.Remove(billType);

        public async Task<IEnumerable<BillType>> GetBillTypeByDescription(string description) => await _context.BillTypes.Where(bt => bt.Description == description).ToListAsync();

        public async Task<BillType> GetBillTypeByIdAsync(int id) => await _context.BillTypes.SingleAsync(bt => bt.Id == id);

        public async Task<IEnumerable<BillType>> GetBillTypesAsync() => await _context.BillTypes.ToListAsync();

        public void Update(BillType billType) => _context.Entry(billType).State = EntityState.Modified;
    }
}