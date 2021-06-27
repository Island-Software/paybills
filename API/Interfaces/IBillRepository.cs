using System.Collections.Generic;
using System.Threading.Tasks;
using API.Entities;

namespace API.Interfaces
{
    // TO-DO: use generics
    public interface IBillRepository
    {
        void Create(Bill bill);
        void Delete(Bill bill);
        void Update(Bill bill);
        // Change it to a base repository class
        Task<bool> SaveAllAsync();
        Task<IEnumerable<Bill>> GetBillsAsync();
        Task<Bill> GetBillByIdAsync(int id);                     
    }
}