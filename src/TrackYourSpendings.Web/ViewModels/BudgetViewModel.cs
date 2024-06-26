using Microsoft.AspNetCore.Mvc.Rendering;
using TrackYourSpendings.Application.Dtos.Transactions;
using TrackYourSpendings.Application.Dtos.Wallets;

namespace TrackYourSpendings.Web.ViewModels;

public class BudgetViewModel
{
    public SelectList? WalletsSelectList { get; set; }
    public SelectList? CategoriesSelectList { get; set; }
    public IEnumerable<GetTransactionDto>? Transactions { get; set; }
    public WalletDto? Wallet { get; set; }
    public Guid WalletId { get; set; }
    public GetTransactionDto Transaction { get; set; } = new();
    public CreateTransactionDto CreateTransactionDto { get; set; } = new();
}