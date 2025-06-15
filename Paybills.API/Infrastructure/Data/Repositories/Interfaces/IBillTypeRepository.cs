using System.Collections.Generic;
using System.Threading.Tasks;
using Paybills.API.Domain.Entities;

namespace Paybills.API.Interfaces
{
    public interface IBillTypeRepository
    {
        Task<bool> CreateAsync(BillType bill);
        void Delete(BillType bill);
        void Update(BillType bill);
        // Change it to a base repository class
        Task<bool> SaveAllAsync();
        Task<IEnumerable<BillType>> GetBillTypesAsync();
        Task<BillType> GetBillTypeByIdAsync(int id);
        Task<IEnumerable<BillType>> GetBillTypeByDescriptionAsync(string description);

        Task<bool> BillTypeExistsAsync(string description);
    }
}