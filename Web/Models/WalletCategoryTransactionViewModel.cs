using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Dtos;

namespace Web.Models;

public class WalletCategoryTransactionViewModel
{
    public SelectList? Wallets { get; set; }
    public SelectList? CategoriesSelectList { get; set; }
    public IEnumerable<TransactionDto>? Transactions { get; set; }
    public Wallet? Wallet { get; set; }
    public int WalletId { get; set; }
    public TransactionDto? Transaction { get; set; }
}