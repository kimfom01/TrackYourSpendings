using Microsoft.AspNetCore.Mvc.Rendering;
using TrackYourSpendings.Web.Dtos;

namespace TrackYourSpendings.Web.Models;

public class WalletCategoryTransactionViewModel
{
    public SelectList? Wallets { get; set; }
    public SelectList? CategoriesSelectList { get; set; }
    public IEnumerable<TransactionDto>? Transactions { get; set; }
    public Wallet? Wallet { get; set; }
    public int WalletId { get; set; }
    public TransactionDto? Transaction { get; set; }
}