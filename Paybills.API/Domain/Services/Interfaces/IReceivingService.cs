using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Paybills.API.Domain.Entities;
using Paybills.API.Helpers;

namespace Paybills.API.Domain.Services.Interfaces
{
    public interface IReceivingService
    {        
        Task<PagedList<Receiving>> GetReceivingsAsync(string username, UserParams userParams);
    }
}