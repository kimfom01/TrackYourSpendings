using System.Linq.Expressions;
using Budget.Context;
using Microsoft.EntityFrameworkCore;

namespace Budget.Repositories;

public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    private readonly BudgetDbContext _budgetDbContext;
    protected DbSet<TEntity> DbEntitySet { get; }

    protected Repository(BudgetDbContext budgetDbContext)
    {
        _budgetDbContext = budgetDbContext;
        DbEntitySet = budgetDbContext.Set<TEntity>();
    }

    public async Task AddEntity(TEntity entity)
    {
        await DbEntitySet.AddAsync(entity);
    }

    public async Task RemoveEntity(int id)
    {
        var entity = await DbEntitySet.FindAsync(id);

        if (entity is not null)
        {
            DbEntitySet.Remove(entity);
        }
    }

    public async Task Update(int id, TEntity entity)
    {
        var exists = await CheckIfExists(id);

        if (exists)
        {
            DbEntitySet.Entry(entity).State = EntityState.Modified;
        }
    }

    public async Task SaveChanges()
    {
        await _budgetDbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<TEntity>?> GetEntities(Expression<Func<TEntity, bool>> predicate)
    {
        var entities = DbEntitySet.Where(predicate).AsNoTracking();

        return await entities.ToListAsync();
    }

    public async Task<TEntity?> GetEntity(int? id)
    {
        var entity = await DbEntitySet.FindAsync(id);

        return entity;
    }

    private async Task<bool> CheckIfExists(int id)
    {
        var entity = await DbEntitySet.FindAsync(id);

        return entity is not null;
    }
}