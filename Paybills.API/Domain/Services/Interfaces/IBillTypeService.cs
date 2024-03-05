using System.Collections.Generic;
using System.Threading.Tasks;
using Paybills.API.Entities;

namespace Paybills.API.Domain.Services.Interfaces
{
    public interface IBillTypeService
    {
        Task<bool> Create(BillType billType);
        Task<bool> Delete(BillType bill);
        Task<bool> Update(BillType bill);
        Task<IEnumerable<BillType>> GetBillTypesAsync();
        Task<BillType> GetBillTypeByIdAsync(int id);
        Task<IEnumerable<BillType>> GetBillTypeByDescription(string description);
        Task<bool> BillTypeExists(string description);
    }
}