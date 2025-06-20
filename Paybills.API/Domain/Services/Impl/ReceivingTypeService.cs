using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Paybills.API.Domain.Entities;
using Paybills.API.Domain.Services.Interfaces;
using Paybills.API.Infrastructure.Data.Repositories.Interfaces;

namespace Paybills.API.Domain.Services.Impl
{
    public class ReceivingTypeService : IReceivingTypeService
    {
        private readonly IReceivingTypeRepository _repository;
        
        public Task<bool> Create(ReceivingType entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(ReceivingType entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Exists(string description)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ReceivingType>> GetAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ReceivingType>> GetByDescription(string description)
        {
            throw new NotImplementedException();
        }

        public Task<ReceivingType> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(ReceivingType entity)
        {
            throw new NotImplementedException();
        }
    }
}