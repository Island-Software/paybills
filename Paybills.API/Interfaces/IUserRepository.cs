using System.Collections.Generic;
using System.Threading.Tasks;
using Paybills.API.Entities;

namespace Paybills.API.Interfaces
{
    public interface IUserRepository
    {
        void Create(AppUser user);
        void Update(AppUser user);
        Task<bool> SaveAllAsync();
        Task<IEnumerable<AppUser>> GetUsersAsync();
        Task<AppUser> GetUserByIdAsync(int id);
        Task<AppUser> GetUserByUsername(string username);
        Task<AppUser> GetUserByUsernameWithDetails(string username);

        Task<bool> Exists(string userName);
    }
}