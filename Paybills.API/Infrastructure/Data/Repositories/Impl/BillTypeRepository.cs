using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Paybills.API.Entities;
using Paybills.API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Paybills.API.Data
{
    public class BillTypeRepository : RepositoryBase, IBillTypeRepository
    {
        public BillTypeRepository(DataContext context) : base(context) { }

        public async Task<bool> BillTypeExistsAsync(string description) => await _context.BillTypes.CountAsync(bt => bt.Description == description) > 0;

        public async Task<bool> CreateAsync(BillType billType) => await _context.BillTypes.AddAsync(billType) != null;

        public void Delete(BillType billType) => _context.BillTypes.Remove(billType);

        public async Task<IEnumerable<BillType>> GetBillTypeByDescriptionAsync(string description) => await _context.BillTypes.Where(bt => bt.Description == description).ToListAsync();

        public async Task<BillType> GetBillTypeByIdAsync(int id) => await _context.BillTypes.SingleOrDefaultAsync(bt => bt.Id == id);

        public async Task<IEnumerable<BillType>> GetBillTypesAsync() => await _context.BillTypes.ToListAsync();

        public void Update(BillType billType) => _context.Entry(billType).State = EntityState.Modified;
    }
}