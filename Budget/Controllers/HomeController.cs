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

    public async Task<IActionResult> Index(int? id)
    {
        WalletCategoryTransactionViewModel viewModel;

        if (id is null or -1)
        {
            var wallets = await _unitOfWork.Wallets.GetEntities(_ => true);

            viewModel = new WalletCategoryTransactionViewModel { Wallets = new SelectList(wallets, "Id", "Name") };
        }
        else
        {
            var wallets = await _unitOfWork.Wallets.GetEntities(_ => true);

            var wallet = await _unitOfWork.Wallets.GetEntity(id);

            var transactions = await _unitOfWork.Transactions.GetEntities(tr => tr.WalletId == wallet.Id);

            var categories = await _unitOfWork.Categories.GetEntities(_ => true);

            viewModel = new WalletCategoryTransactionViewModel
            {
                Wallets = new SelectList(wallets, "Id", "Name"), 
                Wallet = wallet,
                WalletId = wallet.Id,
                CategoriesSelectList = new SelectList(categories, "Id", "Name"),
                Transactions = transactions
            };
        }

        return View(viewModel);
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