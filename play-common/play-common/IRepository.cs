using play_common;
using System.Linq.Expressions;

namespace play_common
{
    public interface IRepository<T> where T : IBaseEntity
    {
        Task CreateAsync(T entity);
        Task<IReadOnlyCollection<T>> GetAllAsync();
        Task<IReadOnlyCollection<T>> GetAllAsync(Expression<Func<T,bool>> filter);
        Task<T> GetByIdAsync(Guid id);
        Task<T> GetByIdAsync( Expression<Func<T, bool>> filter);
        Task RemoveAsync(Guid id);
        Task UpdateAsync(T entity);
    }
}
