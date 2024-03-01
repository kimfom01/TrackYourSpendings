using System.ComponentModel.DataAnnotations;

namespace TrackYourSpendings.Web.Models;

public class Category
{
    public int Id { get; set; }
    
    [MaxLength(50)]
    public required string Name { get; set; }
    public IEnumerable<Transaction>? Transactions { get; set; }
}