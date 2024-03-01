using Microsoft.AspNetCore.Identity;

namespace TrackYourSpendings.Web.Models;

public class ApplicationUser : IdentityUser
{
    public IEnumerable<Transaction>? Transactions { get; set; }
    public IEnumerable<Wallet>? Wallets { get; set; }
}