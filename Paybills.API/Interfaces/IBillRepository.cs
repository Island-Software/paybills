using System.Collections.Generic;
using System.Threading.Tasks;
using Paybills.API.Entities;
using Paybills.API.Helpers;

namespace Paybills.API.Interfaces
{
    // TO-DO: use generics
    public interface IBillRepository
    {
        void Create(Bill bill);
        void Delete(Bill bill);
        void Update(Bill bill);
        Task<bool> CopyBillsToNextMonth(int userId, int currentMonth, int currentYear);
        Task<bool> SaveAllAsync();
        Task<PagedList<Bill>> GetBillsAsync(string username, UserParams userParams);
        Task<PagedList<Bill>> GetBillsByDateAsync(string username, int month, int year, UserParams userParams);
        Task<Bill> GetBillByIdAsync(int id);
        Task<bool> AddBillToUser(int userId, int billId);
        Task<bool> AddBillsToUser(int userId, IEnumerable<Bill> bills);
    }
}