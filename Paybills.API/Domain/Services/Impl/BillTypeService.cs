using System.Collections.Generic;
using System.Threading.Tasks;
using Paybills.API.Domain.Entities;
using Paybills.API.Domain.Services.Interfaces;
using Paybills.API.Interfaces;

namespace Paybills.API.Domain.Services
{
    public class BillTypeService : IBillTypeService
    {
        private readonly IBillTypeRepository _repository;

        public BillTypeService(IBillTypeRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Exists(string description)
        {
            return await _repository.BillTypeExistsAsync(description);
        }

        public async Task<bool> Create(BillType billType)
        {
            await _repository.CreateAsync(billType);

            return await _repository.SaveAllAsync();
        }

        public async Task<bool> Delete(BillType bill)
        {
            _repository.Delete(bill);

            return await _repository.SaveAllAsync();
        }

        public Task<IEnumerable<BillType>> GetByDescription(string description)
        {
            return _repository.GetBillTypeByDescriptionAsync(description);
        }

        public Task<BillType> GetByIdAsync(int id)
        {
            return _repository.GetBillTypeByIdAsync(id);
        }

        public Task<IEnumerable<BillType>> GetAsync()
        {
            return _repository.GetBillTypesAsync();
        }

        public Task<bool> Update(BillType bill)
        {
            _repository.Update(bill);

            return _repository.SaveAllAsync();
        }
    }
}