using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Web.Dtos;
using Web.Models;
using Web.Services;

namespace Web.Controllers;

/// <summary>
/// HomeController manages the main user interface for financial operations including viewing, adding, and modifying wallets and transactions.
/// </summary>
/// <remarks>
/// Requires user authentication. Utilizes services for category, wallet, and transaction management to render the appropriate views.
/// </remarks>
[Authorize]
public class HomeController : Controller
{
    private readonly ICategoryService _categoryService;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IWalletService _walletService;
    private readonly ITransactionService _transactionService;

    /// <summary>
    /// Constructs the HomeController with necessary services.
    /// </summary>
    /// <param name="userManager">Manages user accounts.</param>
    /// <param name="walletService">Provides wallet related operations.</param>
    /// <param name="transactionService">Facilitates transaction management.</param>
    /// <param name="categoryService">Accesses and manipulates financial categories.</param>
    public HomeController(
        UserManager<ApplicationUser> userManager,
        IWalletService walletService,
        ITransactionService transactionService,
        ICategoryService categoryService)
    {
        _userManager = userManager;
        _walletService = walletService;
        _transactionService = transactionService;
        _categoryService = categoryService;
    }

    /// <summary>
    /// Renders the home page, optionally filtering transactions based on search criteria.
    /// </summary>
    /// <param name="searchString">Optional string to search transactions.</param>
    /// <param name="category">Optional category ID to filter transactions.</param>
    /// <param name="date">Optional date to filter transactions.</param>
    /// <returns>The Index view populated with filtered transactions and relevant data.</returns>
    public async Task<IActionResult> Index(string? searchString, int? category, DateTime? date)
    {
        var userId = _userManager.GetUserId(User);

        var wallet = await _walletService.GetActiveWallet(userId);

        var wallets = await _walletService.GetInactiveWallets(userId);

        var categories = await _categoryService.GetCategories();

        if (wallet is null)
        {
            return View(new WalletCategoryTransactionViewModel
            {
                Wallets = new SelectList(wallets, "Id", "Name")
            });
        }

        WalletCategoryTransactionViewModel viewModel;
        if (searchString is not null || category is not null || date is not null)
        {
            viewModel = await GetViewModelFromId(wallet.Id, searchString, category, date);
        }
        else
        {
            viewModel = new WalletCategoryTransactionViewModel
            {
                Wallets = new SelectList(wallets, "Id", "Name"),
                Wallet = wallet,
                CategoriesSelectList = new SelectList(categories, "Id", "Name"),
                Transactions = await _transactionService.GetTransactions(userId)
            };
        }

        return View(viewModel);
    }

    /// <summary>
    /// Changes the active wallet and refreshes the home page with transactions from the selected wallet.
    /// </summary>
    /// <param name="walletId">The ID of the wallet to make active.</param>
    /// <returns>The Index view reflecting the new active wallet's transactions.</returns>
    [HttpPost]
    public async Task<IActionResult> Index(int? walletId)
    {
        if (walletId is null)
        {
            return View("Index");
        }

        var userId = _userManager.GetUserId(User);

        var wallet = await _walletService.GetWallet(walletId, userId);

        if (wallet is null)
        {
            return View("Index");
        }

        await _walletService.SetActiveWallet(wallet, userId);

        var wallets = await _walletService.GetInactiveWallets(userId);

        var categories = await _categoryService.GetCategories();

        var viewModel = new WalletCategoryTransactionViewModel
        {
            Wallets = new SelectList(wallets, "Id", "Name"),
            Wallet = wallet,
            CategoriesSelectList = new SelectList(categories, "Id", "Name"),
            Transactions = await _transactionService.GetTransactions(userId)
        };

        return View(viewModel);
    }

    private async Task<WalletCategoryTransactionViewModel> GetViewModelFromId(int? walletId, string? searchString,
        int? category, DateTime? date)
    {
        var userId = _userManager.GetUserId(User);

        var wallets = await _walletService.GetInactiveWallets(userId);

        var wallet = await _walletService.GetWallet(walletId, userId);

        if (wallet is null)
        {
            return new WalletCategoryTransactionViewModel();
        }

        var transactions =
            await _transactionService.SearchAndFilter(userId, wallet.Id, searchString, category, date);

        var categories = await _categoryService.GetCategories();

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

    /// <summary>
    /// Adds a new wallet for the current user and redirects to the Index view.
    /// </summary>
    /// <param name="wallet">The wallet to add.</param>
    /// <returns>A redirect to the Index view on success, or the Index view with an error message on failure.</returns>
    [HttpPost]
    public async Task<IActionResult> AddWallet(Wallet? wallet)
    {
        try
        {
            if (wallet is null)
            {
                return View("Index");
            }

            var userId = _userManager.GetUserId(User);

            await _walletService.AddWallet(wallet, userId);

            return RedirectToAction("Index");
        }
        catch (Exception)
        {
            return View("Index");
        }
    }

    /// <summary>
    /// Updates an existing wallet's details for the current user and redirects to the Index view.
    /// </summary>
    /// <param name="wallet">The wallet with updated details.</param>
    /// <returns>A redirect to the Index view on success, or the Index view with an error message on failure.</returns>
    [HttpPost]
    public async Task<IActionResult> UpdateWallet(Wallet wallet)
    {
        try
        {
            var userId = _userManager.GetUserId(User);

            await _walletService.UpdateWallet(wallet, userId);

            return RedirectToAction("Index");
        }
        catch (Exception)
        {
            return View("Index");
        }
    }

    /// <summary>
    /// Deletes a wallet for the current user and redirects to the Index view.
    /// </summary>
    /// <param name="wallet">The wallet to delete.</param>
    /// <returns>A redirect to the Index view.</returns>
    [HttpPost]
    public async Task<IActionResult> DeleteWallet(Wallet wallet)
    {
        var userId = _userManager.GetUserId(User);

        await _walletService.DeleteWallet(wallet, userId);

        return RedirectToAction("Index");
    }

    /// <summary>
    /// Adds a new transaction for the current user and redirects to the Index view.
    /// </summary>
    /// <param name="transaction">The transaction to add.</param>
    /// <returns>A redirect to the Index view on success, or the Index view with an error message on failure.</returns>
    [HttpPost]
    public async Task<IActionResult> AddTransaction(Transaction? transaction)
    {
        try
        {
            if (transaction is null)
            {
                throw new Exception("Transaction required");
            }

            var userId = _userManager.GetUserId(User);

            await _transactionService.AddTransaction(transaction, userId);

            return RedirectToAction("Index");
        }
        catch (Exception)
        {
            return View("Index");
        }
    }

    /// <summary>
    /// Updates an existing transaction for the current user and redirects to the Index view.
    /// </summary>
    /// <param name="transaction">The DTO containing transaction updates.</param>
    /// <returns>A redirect to the Index view on success, or the Index view with an error message on failure.</returns>
    [HttpPost]
    public async Task<IActionResult> UpdateTransaction(TransactionDto transaction)
    {
        try
        {
            var userId = _userManager.GetUserId(User);

            await _transactionService.UpdateTransaction(transaction, userId);

            return RedirectToAction("Index");
        }
        catch (Exception)
        {
            return View("Index");
        }
    }

    /// <summary>
    /// Deletes a transaction for the current user and redirects to the Index view.
    /// </summary>
    /// <param name="transaction">The transaction to delete.</param>
    /// <returns>A redirect to the Index view.</returns>
    [HttpPost]
    public async Task<IActionResult> DeleteTransaction(Transaction transaction)
    {
        var userId = _userManager.GetUserId(User);

        await _transactionService.DeleteTransaction(transaction, userId);

        return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}