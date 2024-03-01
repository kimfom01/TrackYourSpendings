using TrackYourSpendings.Web.Models;

namespace TrackYourSpendings.Web.Services;

/// <summary>
/// Provides an abstraction for operations involving <see cref="Category"/> entities.
/// </summary>
/// <remarks>
/// This interface is designed to encapsulate the business logic associated with category management,
/// offering a clear separation between the data access layer and the application's business logic.
/// Through this service, clients can perform high-level operations related to categories.
/// </remarks>
public interface ICategoryService
{
    /// <summary>
    /// Retrieves all categories.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains an enumerable of all <see cref="Category"/> entities.</returns>
    /// <remarks>
    /// This method is intended to provide access to all category entities stored in the repository.
    /// It may be used to display categories in user interfaces or for other business logic requiring information about all categories.
    /// </remarks>
    Task<IEnumerable<Category>> GetCategories();
}
