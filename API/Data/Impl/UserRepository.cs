using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        public UserRepository(DataContext context) : base(context) { }

        public async Task<IEnumerable<AppUser>> GetUsersAsync() => await _context.Users.Include(u => u.Bills).ToListAsync();

        public async Task<AppUser> GetUserByIdAsync(int id) => await _context.Users.Include(u => u.Bills).SingleAsync(u => u.Id == id);

        public async Task<AppUser> GetUserByUsernameAsync(string username) => await _context.Users.Include(u => u.Bills).ThenInclude(b => b.BillType).SingleOrDefaultAsync(u => u.UserName == username);

        public void Update(AppUser user) => _context.Entry(user).State = EntityState.Modified;
    }
}