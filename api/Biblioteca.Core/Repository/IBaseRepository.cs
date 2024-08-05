using System.Linq.Expressions;

namespace Biblioteca.Core.Repository
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(object id);
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> filter);
        Task<TEntity> AddAsync(TEntity entity);
        TEntity Update(TEntity entity);
        Task DeleteAsync(object id);
        Task<int> SaveChangesAsync();
        IQueryable<TEntity> GetQueryable();
    }
}