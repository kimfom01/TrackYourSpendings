using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Budget.Models;
using Budget.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Budget.Controllers;

public class HomeController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public HomeController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IActionResult> Index(int? id, string? searchString, int? category)
    {
        WalletCategoryTransactionViewModel viewModel;

        if (id is null or -1)
        {
            var wallets = await _unitOfWork.Wallets.GetEntities(_ => true);

            viewModel = new WalletCategoryTransactionViewModel { Wallets = new SelectList(wallets, "Id", "Name") };
        }
        else
        {
            viewModel = await GetViewModelFromId(id, searchString, category);
        }

        return View(viewModel);
    }

    private async Task<WalletCategoryTransactionViewModel> GetViewModelFromId(int? id, string? searchString, int? category)
    {
        var wallets = await _unitOfWork.Wallets.GetEntities(_ => true);

        var wallet = await _unitOfWork.Wallets.GetEntity(id);

        if (wallet is null)
        {
            return new WalletCategoryTransactionViewModel();
        }
        
        var transactions = await GetTransactions(wallet.Id, searchString, category);

        var categories = await _unitOfWork.Categories.GetEntities(_ => true);

        var viewModel = new WalletCategoryTransactionViewModel
        {
            Wallets = new SelectList(wallets, "Id", "Name"),
            Wallet = wallet,
            WalletId = wallet.Id,
            CategoriesSelectList = new SelectList(categories, "Id", "Name"),
            Transactions = transactions
        };
        return viewModel;
    }

    private async Task<IEnumerable<Transaction>> GetTransactions(int? id, string? searchString, int? category)
    {
        var transactions = await _unitOfWork.Transactions.GetEntities(tr => tr.WalletId == id);

        if (transactions is null)
        {
            return Enumerable.Empty<Transaction>();
        }
        
        if (searchString is not null)
        {
            transactions = transactions.Where(tr => tr.Name.ToLower().Contains(searchString.ToLower()));
        }

        if (category is not null)
        {
            transactions = transactions.Where(tr => tr.CategoryId == category);
        }

        return transactions;
    }

    [HttpPost]
    public async Task<IActionResult> AddWallet(Wallet wallet)
    {
        await _unitOfWork.Wallets.AddEntity(wallet);
        await _unitOfWork.SaveChanges();
        
        return RedirectToAction("Index");
    }
    
    [HttpPost]
    public async Task<IActionResult> AddTransaction(Transaction transaction, int walletId)
    {
        transaction.Date = DateTime.Now;
        transaction.WalletId = walletId;
        
        await _unitOfWork.Transactions.AddEntity(transaction);

        var wallet = await _unitOfWork.Wallets.GetEntity(walletId);

        wallet.Expenses += transaction.Cost;
        wallet.Balance -= transaction.Cost;
        
        await _unitOfWork.SaveChanges();
        
        return RedirectToAction("Index");
    }
    
    // [HttpPost]
    // public async Task<IActionResult> UpdateTransaction(Transaction transaction, int walletId)
    // {
    //     transaction.Date = DateTime.Now;
    //     
    //     await _unitOfWork.Transactions.Update(transaction.Id, transaction);
    //
    //     var wallet = await _unitOfWork.Wallets.GetEntity(walletId);
    //
    //     wallet.Expenses += transaction.Cost;
    //     wallet.Balance -= transaction.Cost;
    //     
    //     await _unitOfWork.SaveChanges();
    //     
    //     return RedirectToAction("Index");
    // }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}