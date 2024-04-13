using System.Collections.Generic;
using System.Threading.Tasks;
using Paybills.API.Entities;
using Paybills.API.Helpers;

namespace Paybills.API.Interfaces
{
    // TO-DO: use generics
    public interface IBillRepository
    {
        Task<bool> CreateAsync(Bill bill);
        Task<bool> DeleteAsync(Bill bill);
        Task<bool> UpdateAsync(Bill bill);
        Task<bool> CopyBillsToNextMonthAsync(int userId, int currentMonth, int currentYear);
        Task<bool> SaveAllAsync();
        Task<PagedList<Bill>> GetBillsAsync(string username, UserParams userParams);
        Task<PagedList<Bill>> GetBillsByDateAsync(string username, int month, int year, UserParams userParams);
        Task<Bill> GetBillByIdAsync(int id);
        Task<bool> AddBillToUserAsync(int userId, int billId);
        Task<bool> AddBillsToUserAsync(int userId, IEnumerable<Bill> bills);
    }
}