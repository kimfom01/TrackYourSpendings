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

    public virtual async Task AddEntity(TEntity entity)
    {
        await DbEntitySet.AddAsync(entity);
    }

    public virtual async Task RemoveEntity(Expression<Func<TEntity, bool>> predicate)
    {
        var entity = await DbEntitySet.FirstOrDefaultAsync(predicate);

        if (entity is not null)
        {
            DbEntitySet.Remove(entity);
        }
    }

    public virtual Task Update(int id, TEntity entity)
    {
        DbEntitySet.Update(entity);

        return Task.CompletedTask;
    }

    public virtual async Task SaveChanges()
    {
        await _dataContext.SaveChangesAsync();
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