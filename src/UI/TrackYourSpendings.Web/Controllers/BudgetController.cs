using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TrackYourSpendings.Application.Dtos.Transactions;
using TrackYourSpendings.Application.Dtos.Wallets;
using TrackYourSpendings.Application.Features.Categories.Requests.Queries;
using TrackYourSpendings.Application.Features.Transactions.Requests.Commands;
using TrackYourSpendings.Application.Features.Transactions.Requests.Queries;
using TrackYourSpendings.Application.Features.Wallets.Requests.Commands;
using TrackYourSpendings.Application.Features.Wallets.Requests.Queries;
using TrackYourSpendings.Infrastructure.Identity;
using TrackYourSpendings.Web.ViewModels;

namespace TrackYourSpendings.Web.Controllers;

/// <summary>
/// BudgetController manages the main user interface for financial operations including viewing, adding, and modifying wallets and transactions.
/// </summary>
/// <remarks>
/// Requires user authentication. Utilizes services for category, wallet, and transaction management to render the appropriate views.
/// </remarks>
[Authorize]
public class BudgetController : Controller
{
    private readonly IMediator _mediator;
    private readonly UserManager<ApplicationUser> _userManager;

    private const string IndexPage = "Index";

    /// <summary>
    /// Constructs the HomeController with necessary services.
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="userManager"></param>
    public BudgetController(
        IMediator mediator,
        UserManager<ApplicationUser> userManager)
    {
        _mediator = mediator;
        _userManager = userManager;
    }

    /// <summary>
    /// Renders the home page, optionally filtering transactions based on search criteria.
    /// </summary>
    /// <param name="searchString">Optional string to search transactions.</param>
    /// <param name="category">Optional category ID to filter transactions.</param>
    /// <param name="date">Optional date to filter transactions.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>The Index view populated with filtered transactions and relevant data.</returns>
    public async Task<IActionResult> Index(string? searchString, Guid? category, DateTime? date,
        CancellationToken cancellationToken)
    {
        var userId = new Guid(_userManager.GetUserId(User));

        var wallet = await _mediator.Send(new GetActiveWalletRequest { UserId = userId }, cancellationToken);

        var wallets = await _mediator.Send(new GetInactiveWalletsRequest { UserId = userId }, cancellationToken);

        var categories = await _mediator.Send(new GetCatetoriesRequest { UserId = userId }, cancellationToken);

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
            viewModel = await GetViewModelFromId(wallet.Id, searchString, category, date, cancellationToken);
        }
        else
        {
            viewModel = new WalletCategoryTransactionViewModel
            {
                Wallets = new SelectList(wallets, "Id", "Name"),
                Wallet = wallet,
                CategoriesSelectList = new SelectList(categories, "Id", "Name"),
                Transactions = await _mediator.Send(new GetTransactionsRequest { UserId = userId }, cancellationToken)
            };
        }

        return View(viewModel);
    }

    /// <summary>
    /// Changes the active wallet and refreshes the home page with transactions from the selected wallet.
    /// </summary>
    /// <param name="walletId">The ID of the wallet to make active.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>The Index view reflecting the new active wallet's transactions.</returns>
    [HttpPost]
    public async Task<IActionResult> Index(Guid? walletId, CancellationToken cancellationToken)
    {
        if (walletId is null)
        {
            return View(IndexPage);
        }

        var userId = new Guid(_userManager.GetUserId(User));

        var wallet = await _mediator.Send(new GetWalletRequest
        {
            UserId = userId, WalletId = walletId.Value
        }, cancellationToken);

        if (wallet is null)
        {
            return View(IndexPage);
        }

        await _mediator.Send(new SetActiveWalletRequest
        {
            UserId = userId, WalletId = walletId.Value
        }, cancellationToken);

        var wallets = await _mediator.Send(new GetInactiveWalletsRequest { UserId = userId }, cancellationToken);

        var categories = await _mediator.Send(new GetCatetoriesRequest { UserId = userId }, cancellationToken);

        var viewModel = new WalletCategoryTransactionViewModel
        {
            Wallets = new SelectList(wallets, "Id", "Name"),
            Wallet = wallet,
            CategoriesSelectList = new SelectList(categories, "Id", "Name"),
            Transactions = await _mediator.Send(new GetTransactionsRequest { UserId = userId }, cancellationToken)
        };

        return View(viewModel);
    }

    private async Task<WalletCategoryTransactionViewModel> GetViewModelFromId(Guid? walletId, string? searchString,
        Guid? categoryId, DateTime? date, CancellationToken cancellationToken)
    {
        var userId = new Guid(_userManager.GetUserId(User));

        var wallets = await _mediator.Send(new GetInactiveWalletsRequest { UserId = userId }, cancellationToken);

        var wallet = await _mediator.Send(new GetWalletRequest
        {
            UserId = userId,
            WalletId = walletId.Value
        }, cancellationToken);

        if (wallet is null)
        {
            return new WalletCategoryTransactionViewModel();
        }

        var transactions =
            await _mediator.Send(new SearchAndFilterTransactionsRequest
            {
                CategoryId = categoryId,
                Date = date,
                SearchString = searchString,
                UserId = userId,
                WalletId = walletId
            }, cancellationToken);

        var categories = await _mediator.Send(new GetCatetoriesRequest { UserId = userId }, cancellationToken);

        var viewModel = new WalletCategoryTransactionViewModel
        {
            Wallets = new SelectList(wallets, "Id", "Name"),
            Wallet = wallet,
            WalletId = walletId.Value,
            CategoriesSelectList = new SelectList(categories, "Id", "Name"),
            Transactions = transactions
        };

        return viewModel;
    }

    /// <summary>
    /// Adds a new wallet for the current user and redirects to the Index view.
    /// </summary>
    /// <param name="wallet">The wallet to add.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>A redirect to the Index view on success, or the Index view with an error message on failure.</returns>
    [HttpPost]
    public async Task<IActionResult> AddWallet(WalletCreateDto? wallet, CancellationToken cancellationToken)
    {
        try
        {
            if (wallet is null)
            {
                return View(IndexPage);
            }

            var userId = new Guid(_userManager.GetUserId(User));

            await _mediator.Send(new WalletCreateRequest
            {
                UserId = userId,
                WalletCreateDto = wallet
            }, cancellationToken);

            return RedirectToAction(IndexPage);
        }
        catch (Exception)
        {
            return View(IndexPage);
        }
    }

    /// <summary>
    /// Updates an existing wallet's details for the current user and redirects to the Index view.
    /// </summary>
    /// <param name="wallet">The wallet with updated details.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>A redirect to the Index view on success, or the Index view with an error message on failure.</returns>
    [HttpPost]
    public async Task<IActionResult> UpdateWallet(WalletUpdateDto wallet,
        CancellationToken cancellationToken)
    {
        try
        {
            var userId = new Guid(_userManager.GetUserId(User));

            await _mediator.Send(new UpdateWalletRequest
            {
                UserId = userId,
                WalletId = wallet.Id,
                WalletUpdateDto = wallet
            }, cancellationToken);

            return RedirectToAction("Index");
        }
        catch (Exception)
        {
            return View(IndexPage);
        }
    }

    /// <summary>
    /// Deletes a wallet for the current user and redirects to the Index view.
    /// </summary>
    /// <param name="wallet">The wallet to delete.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>A redirect to the Index view.</returns>
    [HttpPost]
    public async Task<IActionResult> DeleteWallet(WalletDto wallet, CancellationToken cancellationToken)
    {
        var userId = new Guid(_userManager.GetUserId(User));

        await _mediator.Send(new DeleteWalletRequest
        {
            UserId = userId,
            WalletId = wallet.Id
        }, cancellationToken);

        return RedirectToAction("Index");
    }

    /// <summary>
    /// Adds a new transaction for the current user and redirects to the Index view.
    /// </summary>
    /// <param name="transaction">The transaction to add.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>A redirect to the Index view on success, or the Index view with an error message on failure.</returns>
    [HttpPost]
    public async Task<IActionResult> AddTransaction(CreateTransactionDto? transaction,
        CancellationToken cancellationToken)
    {
        try
        {
            if (transaction is null)
            {
                return View(IndexPage);
            }

            var userId = new Guid(_userManager.GetUserId(User));

            await _mediator.Send(new CreateTransactionRequest
            {
                TransactionDto = transaction,
                UserId = userId
            }, cancellationToken);

            return RedirectToAction(IndexPage);
        }
        catch (Exception)
        {
            return View(IndexPage);
        }
    }

    /// <summary>
    /// Updates an existing transaction for the current user and redirects to the Index view.
    /// </summary>
    /// <param name="transaction">The DTO containing transaction updates.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>A redirect to the Index view on success, or the Index view with an error message on failure.</returns>
    [HttpPost]
    public async Task<IActionResult> UpdateTransaction(GetTransactionDto transaction,
        CancellationToken cancellationToken)
    {
        try
        {
            var userId = new Guid(_userManager.GetUserId(User));

            await _mediator.Send(new UpdateTransactionRequest
            {
                TransactionDto = transaction,
                UserId = userId
            }, cancellationToken);

            return RedirectToAction(IndexPage);
        }
        catch (Exception)
        {
            return View(IndexPage);
        }
    }

    /// <summary>
    /// Deletes a transaction for the current user and redirects to the Index view.
    /// </summary>
    /// <param name="transaction">The transaction to delete.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>A redirect to the Index view.</returns>
    [HttpPost]
    public async Task<IActionResult> DeleteTransaction(GetTransactionDto transaction,
        CancellationToken cancellationToken)
    {
        var userId = new Guid(_userManager.GetUserId(User));

        await _mediator.Send(new DeleteTransactionRequest
            {
                TransactionDto = transaction,
                UserId = userId
            },
            cancellationToken);

        return RedirectToAction(IndexPage);
    }
}