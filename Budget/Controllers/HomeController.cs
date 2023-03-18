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

        if (id == null)
        {
            var wallets = await _unitOfWork.Wallets.GetEntities(_ => true);

            viewModel = new WalletCategoryTransactionViewModel { Wallets = new SelectList(wallets, "Id", "Name") };
        }
        else if (id != -1)
        {
            var wallets = await _unitOfWork.Wallets.GetEntities(_ => true);

            var wallet = await _unitOfWork.Wallets.GetEntity(id);

            var categories = await _unitOfWork.Categories.GetEntities(c => c.WalletId == wallet.Id);

            var transactions = await _unitOfWork.Transactions.GetEntities(_ => true);

            viewModel = new WalletCategoryTransactionViewModel
            {
                Wallets = new SelectList(wallets, "Id", "Name"), Wallet = wallet,
                CategoriesSelectList = new SelectList(categories, "CategoryId", "CategoryName"),
                Transactions = transactions
            };
        }
        else
        {
            viewModel = new WalletCategoryTransactionViewModel();
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
    // [IgnoreAntiforgeryToken]
    public async Task<IActionResult> AddTransaction(Transaction transaction)
    {
        transaction.Date = DateTime.Now;
        
        await _unitOfWork.Transactions.AddEntity(transaction);
        await _unitOfWork.SaveChanges();

        return RedirectToAction("Index");
    }

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