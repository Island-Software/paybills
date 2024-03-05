using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Paybills.API.Domain.Services.Interfaces;
using Paybills.API.Entities;
using Paybills.API.Helpers;
using Paybills.API.Interfaces;

namespace Paybills.API.Domain.Services.Impl
{
    public class BillService : IBillService
    {
        private readonly IBillRepository _repository;

        public BillService(IBillRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> AddBillsToUser(int userId, IEnumerable<Bill> bills) => await _repository.AddBillsToUser(userId, bills);

        public async Task<bool> AddBillToUser(int userId, int billId) => await _repository.AddBillToUser(userId, billId);

        public async Task<bool> CopyBillsToNextMonth(int userId, int currentMonth, int currentYear) => await _repository.CopyBillsToNextMonth(userId, currentMonth, currentYear);

        public async Task<bool> Create(Bill bill) => await _repository.Create(bill);

        public Task<bool> Delete(Bill bill) => _repository.Delete(bill);

        public async Task<Bill> GetBillByIdAsync(int id) => await _repository.GetBillByIdAsync(id);

        public Task<PagedList<Bill>> GetBillsAsync(string username, UserParams userParams) => _repository.GetBillsAsync(username, userParams);

        public Task<PagedList<Bill>> GetBillsByDateAsync(string username, int month, int year, UserParams userParams) => _repository.GetBillsByDateAsync(username, month, year, userParams);

        public Task<bool> Update(Bill bill) => _repository.Update(bill);
    }
}