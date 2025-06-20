using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Paybills.API.Domain.Entities;

namespace Paybills.API.Infrastructure.Data.Repositories.Interfaces
{
    public interface IReceivingTypeRepository
    {
        Task<bool> CreateAsync(ReceivingType receivingType);
        void Delete(ReceivingType receivingType);
        void Update(ReceivingType receivingType);
        // Change it to a base repository class
        Task<bool> SaveAllAsync();
        Task<IEnumerable<ReceivingType>> GetAsync();
        Task<ReceivingType> GetByIdAsync(int id);
        Task<IEnumerable<ReceivingType>> GetByDescriptionAsync(string description);

        Task<bool> ExistsAsync(string description);
    }
}