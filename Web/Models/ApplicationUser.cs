using Microsoft.AspNetCore.Identity;

namespace Web.Models;

public class ApplicationUser : IdentityUser
{
    public IEnumerable<Transaction>? Transactions { get; set; }
    public IEnumerable<Category>? Categories { get; set; }
    public IEnumerable<Wallet>? Wallets { get; set; }
}