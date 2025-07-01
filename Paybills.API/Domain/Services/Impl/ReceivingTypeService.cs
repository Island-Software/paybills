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

        public ReceivingTypeService(IReceivingTypeRepository repository)
        {
            _repository = repository;
        }
        
        public async Task<bool> Create(ReceivingType entity)
        {
            await _repository.CreateAsync(entity);

            return await _repository.SaveAllAsync();
        }

        public Task<bool> Delete(ReceivingType entity)
        {
            _repository.Delete(entity);
            return _repository.SaveAllAsync();
        }

        public Task<bool> Exists(string description)
        {
            return _repository.ExistsAsync(description);
        }

        public Task<IEnumerable<ReceivingType>> GetAsync()
        {
            return _repository.GetAsync();
        }

        public Task<IEnumerable<ReceivingType>> GetByDescription(string description)
        {
            return _repository.GetByDescriptionAsync(description);
        }

        public Task<ReceivingType> GetByIdAsync(int id)
        {
            return _repository.GetByIdAsync(id);
        }

        public Task<bool> Update(ReceivingType entity)
        {
            _repository.Update(entity);
            return _repository.SaveAllAsync();
        }
    }
}