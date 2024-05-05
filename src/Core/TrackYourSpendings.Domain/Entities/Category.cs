using System.ComponentModel.DataAnnotations;
using TrackYourSpendings.Domain.Entities.Common;

namespace TrackYourSpendings.Domain.Entities;

public class Category : BaseEntity
{
    [MaxLength(50)] public string Name { get; set; } = string.Empty;
    public IEnumerable<Transaction>? Transactions { get; set; }
    [MaxLength(128)] public Guid UserId { get; set; }
}