using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TrackYourSpendings.Application.Dtos.Wallets;
using TrackYourSpendings.Application.Features.Wallets.Requests.Commands;
using TrackYourSpendings.Infrastructure.Database.Identity;

namespace TrackYourSpendings.Web.Controllers;

[Authorize]
public class WalletController : Controller
{
    private readonly IMediator _mediator;
    private readonly UserManager<ApplicationUser> _userManager;

    private const string IndexPage = "Index";

    public WalletController(IMediator mediator, UserManager<ApplicationUser> userManager)
    {
        _mediator = mediator;
        _userManager = userManager;
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
                return RedirectToAction(IndexPage, "Budget");
            }

            var userId = _userManager.GetUserId(User);

            await _mediator.Send(new WalletCreateRequest
            {
                UserId = userId,
                WalletCreateDto = wallet
            }, cancellationToken);

            return RedirectToAction(IndexPage, "Budget");
        }
        catch (Exception)
        {
            return RedirectToAction(IndexPage, "Budget");
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
            var userId = _userManager.GetUserId(User);

            await _mediator.Send(new UpdateWalletRequest
            {
                UserId = userId,
                WalletId = wallet.Id,
                WalletUpdateDto = wallet
            }, cancellationToken);

            return RedirectToAction(IndexPage, "Budget");
        }
        catch (Exception)
        {
            return RedirectToAction(IndexPage, "Budget");
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
        var userId = _userManager.GetUserId(User);

        await _mediator.Send(new DeleteWalletRequest
        {
            UserId = userId,
            WalletId = wallet.Id
        }, cancellationToken);

        return RedirectToAction(IndexPage, "Budget");
    }
}