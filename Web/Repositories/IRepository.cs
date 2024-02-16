using System.Linq.Expressions;

namespace Web.Repositories;

public interface IRepository<TEntity> where TEntity : class
{
    Task AddEntity(TEntity entity);
    Task RemoveEntity(Expression<Func<TEntity, bool>> predicate);
    Task Update(int id, TEntity entity);
    Task<IEnumerable<TEntity>?> GetEntities(Expression<Func<TEntity, bool>> predicate);
    Task<TEntity?> GetEntity(Expression<Func<TEntity, bool>> predicate);
}