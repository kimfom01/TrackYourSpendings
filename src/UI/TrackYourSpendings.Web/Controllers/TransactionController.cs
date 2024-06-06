using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TrackYourSpendings.Application.Dtos.Transactions;
using TrackYourSpendings.Application.Features.Transactions.Requests.Commands;
using TrackYourSpendings.Infrastructure.Database.Identity;

namespace TrackYourSpendings.Web.Controllers;

[Authorize]
public class TransactionController : Controller
{
    private readonly IMediator _mediator;
    private readonly UserManager<ApplicationUser> _userManager;

    private const string IndexPage = "Index";

    public TransactionController(IMediator mediator, UserManager<ApplicationUser> userManager)
    {
        _mediator = mediator;
        _userManager = userManager;
    }

    /// <summary>
    /// Adds a new transaction for the current user and redirects to the Index view.
    /// </summary>
    /// <param name="createTransactionDto"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>A redirect to the Index view on success, or the Index view with an error message on failure.</returns>
    [HttpPost]
    public async Task<IActionResult> AddTransaction(CreateTransactionDto? createTransactionDto,
        CancellationToken cancellationToken)
    {
        try
        {
            if (createTransactionDto is null)
            {
                return RedirectToAction(IndexPage, "Budget");
            }

            var userId = _userManager.GetUserId(User);

            await _mediator.Send(new CreateTransactionRequest
            {
                TransactionDto = createTransactionDto,
                UserId = userId
            }, cancellationToken);

            return RedirectToAction(IndexPage, "Budget");
        }
        catch (Exception)
        {
            return RedirectToAction(IndexPage, "Budget");
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
            var userId = _userManager.GetUserId(User);

            await _mediator.Send(new UpdateTransactionRequest
            {
                TransactionDto = transaction,
                UserId = userId
            }, cancellationToken);

            return RedirectToAction(IndexPage, "Budget");
        }
        catch (Exception)
        {
            return RedirectToAction(IndexPage, "Budget");
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
        var userId = _userManager.GetUserId(User);

        await _mediator.Send(new DeleteTransactionRequest
            {
                TransactionDto = transaction,
                UserId = userId
            },
            cancellationToken);

        return RedirectToAction(IndexPage, "Budget");
    }
}