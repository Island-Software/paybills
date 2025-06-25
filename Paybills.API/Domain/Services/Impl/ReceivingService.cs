using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Paybills.API.Domain.Entities;
using Paybills.API.Domain.Services.Interfaces;
using Paybills.API.Helpers;
using Paybills.API.Infrastructure.Data.Repositories.Interfaces;

namespace Paybills.API.Domain.Services.Impl
{
    public class ReceivingService : IReceivingService
    {
        private readonly IReceivingRepository _repository;
        
        public ReceivingService(IReceivingRepository repository)
        {
            _repository = repository;
        }

        public Task<bool> AddToUser(int userId, int receivingId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddToUser(int userId, IEnumerable<Receiving> receivings)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CopyToNextMonth(int userId, int currentMonth, int currentYear)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Create(Receiving receiving)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(Receiving receiving)
        {
            throw new NotImplementedException();
        }

        public Task<PagedList<Receiving>> GetAsync(string username, UserParams userParams)
        {
            throw new NotImplementedException();
        }

        public Task<PagedList<Receiving>> GetByDateAsync(string username, int month, int year, UserParams userParams)
        {
            throw new NotImplementedException();
        }

        public Task<Receiving> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(Receiving receiving)
        {
            throw new NotImplementedException();
        }
    }
}