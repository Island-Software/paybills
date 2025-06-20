using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Paybills.API.Domain.Entities;
using Paybills.API.Domain.Services.Interfaces;
using Paybills.API.Helpers;

namespace Paybills.API.Domain.Services.Impl
{
    public class ReceivingService : IReceivingService
    {
        public Task<PagedList<Receiving>> GetReceivingsAsync(string username, UserParams userParams)
        {
            throw new NotImplementedException();
        }
    }
}