using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TrackYourSpendings.Application.Contracts.Database.Repository;

namespace TrackYourSpendings.Infrastructure.Database.Repositories;

public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    protected DbSet<TEntity> DbEntitySet { get; }

    protected Repository(AppDataContext appDataContext)
    {
        DbEntitySet = appDataContext.Set<TEntity>();
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

    public virtual async Task<List<TEntity>?> GetEntities(Expression<Func<TEntity, bool>> predicate)
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