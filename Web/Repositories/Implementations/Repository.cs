using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Web.Context;

namespace Web.Repositories.Implementations;

public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    private readonly DataContext _dataContext;
    protected DbSet<TEntity> DbEntitySet { get; }

    protected Repository(DataContext dataContext)
    {
        _dataContext = dataContext;
        DbEntitySet = dataContext.Set<TEntity>();
    }

    public virtual async Task<TEntity> AddEntity(TEntity entity)
    {
        var entityEntry = await DbEntitySet.AddAsync(entity);

        return entityEntry.Entity;
    }

    public virtual async Task RemoveEntity(Expression<Func<TEntity, bool>> predicate)
    {
        var entity = await DbEntitySet.FirstOrDefaultAsync(predicate);

        if (entity is not null)
        {
            DbEntitySet.Remove(entity);
        }
    }

    public virtual Task Update(TEntity entity)
    {
        DbEntitySet.Update(entity);

        return Task.CompletedTask;
    }

    public virtual async Task<IEnumerable<TEntity>?> GetEntities(Expression<Func<TEntity, bool>> predicate)
    {
        var entities = DbEntitySet.Where(predicate).AsNoTracking();

        return await entities.ToListAsync();
    }

    public virtual async Task<TEntity?> GetEntity(Expression<Func<TEntity, bool>> predicate)
    {
        var entity = await DbEntitySet.FirstOrDefaultAsync(predicate);

        return entity;
    }
}