using System.ComponentModel.DataAnnotations;

namespace TrackYourSpendings.Domain.Entities.Common;

public abstract class BaseEntity
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }
    [MaxLength(128)] public string UserId { get; set; } = string.Empty;
}