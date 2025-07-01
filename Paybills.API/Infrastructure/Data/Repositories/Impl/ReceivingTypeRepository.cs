using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.Runtime;
using Microsoft.EntityFrameworkCore;
using Paybills.API.Data;
using Paybills.API.Domain.Entities;
using Paybills.API.Infrastructure.Data.Repositories.Interfaces;

namespace Paybills.API.Infrastructure.Data.Repositories.Impl
{
    public class ReceivingTypeRepository : RepositoryBase, IReceivingTypeRepository
    {
        public ReceivingTypeRepository(DataContext context) : base(context) { }

        public async Task<bool> CreateAsync(ReceivingType receivingType)
        {
            return await _context.ReceivingTypes.AddAsync(receivingType) != null;
        }

        public void Delete(ReceivingType receivingType)
        {
            _context.ReceivingTypes.Remove(receivingType);
        }

        public async Task<bool> ExistsAsync(string description)
        {
            return await _context.ReceivingTypes.CountAsync(bt => bt.Description == description) > 0;
        }

        public async Task<IEnumerable<ReceivingType>> GetAsync()
        {
            return await _context.ReceivingTypes.ToListAsync();
        }

        public async Task<IEnumerable<ReceivingType>> GetByDescriptionAsync(string description)
        {
            return await _context.ReceivingTypes.Where(bt => bt.Description == description).ToListAsync();
        }

        public async Task<ReceivingType> GetByIdAsync(int id)
        {
            return await _context.ReceivingTypes.SingleOrDefaultAsync(bt => bt.Id == id);
        }

        public void Update(ReceivingType receivingType)
        {
            _context.Entry(receivingType).State = EntityState.Modified;
        }
    }
}