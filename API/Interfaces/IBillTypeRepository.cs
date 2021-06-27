using System.Collections.Generic;
using System.Threading.Tasks;
using API.Entities;

namespace API.Interfaces
{
    public interface IBillTypeRepository
    {
        void Create(BillType bill);
        void Delete(BillType bill);
        void Update(BillType bill);
        // Change it to a base repository class
        Task<bool> SaveAllAsync();
        Task<IEnumerable<BillType>> GetBillTypesAsync();
        Task<BillType> GetBillTypeByIdAsync(int id); 
    }
}