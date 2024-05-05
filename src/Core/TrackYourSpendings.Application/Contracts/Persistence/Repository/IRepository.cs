using System.Linq.Expressions;

namespace TrackYourSpendings.Application.Contracts.Persistence.Repository;

/// <summary>
/// Defines a generic repository interface for managing entities of type <typeparamref name="TEntity"/>.
/// </summary>
/// <typeparam name="TEntity">The type of the entity this repository manages. Must be a class.</typeparam>
/// <remarks>
/// This interface abstracts the common database operations such as add, remove, update, and retrieval of entities,
/// promoting a more decoupled and testable architecture in applications.
/// </remarks>
public interface IRepository<TEntity> where TEntity : class
{
    /// <summary>
    /// Adds a new entity to the repository.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    /// <returns>The added entity.</returns>
    Task<TEntity> AddEntity(TEntity entity);

    /// <summary>
    /// Removes entities from the repository that satisfy the specified predicate.
    /// </summary>
    /// <param name="predicate">A function to test each entity for a condition.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task RemoveEntity(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// Updates an existing entity in the repository.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task Update(TEntity entity);

    /// <summary>
    /// Retrieves all entities from the repository that satisfy the specified predicate.
    /// </summary>
    /// <param name="predicate">A function to test each entity for a condition.</param>
    /// <returns>A task that represents the asynchronous operation, containing the list of entities.</returns>
    Task<List<TEntity>?> GetEntities(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// Retrieves a single entity from the repository that satisfies the specified predicate.
    /// </summary>
    /// <param name="predicate">A function to test each entity for a condition.</param>
    /// <returns>A task that represents the asynchronous operation, containing the entity if found; otherwise, null.</returns>
    Task<TEntity?> GetEntity(Expression<Func<TEntity, bool>> predicate);
}
