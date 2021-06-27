using System.Threading.Tasks;

namespace API.Data
{
    public class RepositoryBase
    {
        protected readonly DataContext _context;
        public RepositoryBase(DataContext context)
        {
            this._context = context;
        }

        public async Task<bool> SaveAllAsync() => await _context.SaveChangesAsync() > 0;
    }
}