using Domain.Entities;

namespace Application.Contracts.Repositories.Common
{
    public interface IBaseRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        Task<TEntity> CreateAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<TEntity?> GetByIdAsync(TKey id, Func<IQueryable<TEntity>, IQueryable<TEntity>>? includeExpression = null);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> DeleteAsync(TEntity entity);
    }
}