using System.Collections.Generic;
using System.Threading.Tasks;
using Paybills.API.Domain.Services.Interfaces;
using Paybills.API.Entities;
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

        public Task<bool> BillTypeExists(string description)
        {
            throw new System.NotImplementedException();
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

        public Task<IEnumerable<BillType>> GetBillTypeByDescription(string description)
        {
            return _repository.GetBillTypeByDescriptionAsync(description);
        }

        public Task<BillType> GetBillTypeByIdAsync(int id)
        {
            return _repository.GetBillTypeByIdAsync(id);
        }

        public Task<IEnumerable<BillType>> GetBillTypesAsync()
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