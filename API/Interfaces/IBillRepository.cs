using System.Collections.Generic;
using System.Threading.Tasks;
using API.Entities;
using API.Helpers;

namespace API.Interfaces
{
    // TO-DO: use generics
    public interface IBillRepository
    {
        void Create(Bill bill);
        void Delete(Bill bill);
        void Update(Bill bill);
        Task<bool> SaveAllAsync();
        Task<PagedList<Bill>> GetBillsAsync(UserParams userParams);
        Task<Bill> GetBillByIdAsync(int id);                     
    }
}