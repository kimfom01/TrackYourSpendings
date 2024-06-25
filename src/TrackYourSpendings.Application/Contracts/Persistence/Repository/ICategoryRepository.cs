using TrackYourSpendings.Domain.Entities;

namespace TrackYourSpendings.Application.Contracts.Persistence.Repository;

/// <summary>
/// Defines the contract for a repository managing <see cref="Category"/> entities, extending the common CRUD operations provided by the <see cref="IRepository{TEntity}"/>.
/// </summary>
/// <remarks>
/// This interface is tailored to handle data operations for Category entities. While it currently does not specify additional methods beyond the generic repository interface, it sets the foundation for future category-specific data access requirements and enhances the application's architecture by segregating repository responsibilities.
/// </remarks>
public interface ICategoryRepository : IRepository<Category>
{
}