using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Paybills.API.Entities;
using Paybills.API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Paybills.API.Data
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        public UserRepository(DataContext context) : base(context) { }

        public async Task<IEnumerable<AppUser>> GetUsersAsync() => await _context.Users.Include(u => u.Bills).ToListAsync();

        public async Task<AppUser> GetUserByIdAsync(int id) => await _context.Users.Include(u => u.Bills).SingleAsync(u => u.Id == id);

        public async Task<AppUser> GetUserByUsernameWithDetails(string username)
        {
            return await _context.Users
                .Include(u => u.Bills)
                .ThenInclude(b => b.BillType)
                .SingleOrDefaultAsync(u => u.UserName == username);
        }

        public void Update(AppUser user) => _context.Entry(user).State = EntityState.Modified;

        public void Create(AppUser user) => _context.Users.Add(user);

        public async Task<bool> Exists(string userName) => await _context.Users.AnyAsync(user => user.UserName == userName.ToLower());

        public async Task<AppUser> GetUserByUsername(string username) => await _context.Users.SingleOrDefaultAsync(u => u.UserName.ToLower() == username.ToLower());
    }
}