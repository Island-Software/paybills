using System.Collections.Generic;
using System.Threading.Tasks;

namespace Paybills.API.Domain.Services.Interfaces
{
    public interface IBaseCrudService<T>
    {
        Task<bool> Create(T entity);
        Task<bool> Delete(T entity);
        Task<bool> Update(T entity);
        Task<IEnumerable<T>> GetAsync();
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetByDescription(string description);
        Task<bool> Exists(string description);
    }
}