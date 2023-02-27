using System.Linq.Expressions;
using Budget.Data;
using Microsoft.EntityFrameworkCore;

namespace Budget.Repositories;

public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    private readonly BudgetDbContext _budgetDbContext;
    private readonly DbSet<TEntity> _dbEntitySet;

    protected Repository(BudgetDbContext budgetDbContext)
    {
        _budgetDbContext = budgetDbContext;
        _dbEntitySet = budgetDbContext.Set<TEntity>();
    }

    public async Task AddEntity(TEntity entity)
    {
        await _dbEntitySet.AddAsync(entity);
    }

    public async Task RemoveEntity(int id)
    {
        var entity = await _dbEntitySet.FindAsync(id);

        if (entity is not null)
        {
            _dbEntitySet.Remove(entity);
        }
    }

    public async Task Update(int id, TEntity entity)
    {
        var exists = await CheckIfExists(id);

        if (exists)
        {
            _dbEntitySet.Entry(entity).State = EntityState.Modified;
        }
    }

    public async Task SaveChanges()
    {
        await _budgetDbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<TEntity>?> GetEntities(Expression<Func<TEntity, bool>> predicate)
    {
        var entities = _dbEntitySet.Where(predicate).AsNoTracking();

        return await entities.ToListAsync();
    }

    public async Task<TEntity?> GetEntity(int id)
    {
        var entity = await _dbEntitySet.FindAsync(id);

        return entity;
    }

    private async Task<bool> CheckIfExists(int id)
    {
        var entity = await _dbEntitySet.FindAsync(id);

        return entity is not null;
    }
}