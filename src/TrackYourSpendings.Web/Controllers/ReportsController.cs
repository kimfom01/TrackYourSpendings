using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TrackYourSpendings.Application.Dtos.Wallets;
using TrackYourSpendings.Application.Features.Wallets.Requests.Queries;
using TrackYourSpendings.Domain.Entities;
using TrackYourSpendings.Infrastructure.Database.Identity;

namespace TrackYourSpendings.Web.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class ReportsController : Controller
{
    private readonly IMediator _mediator;
    private readonly UserManager<ApplicationUser> _userManager;

    public ReportsController(
        IMediator mediator,
        UserManager<ApplicationUser> userManager
    )
    {
        _mediator = mediator;
        _userManager = userManager;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet("wallets/q")]
    public async Task<ActionResult<WalletDto>> GetWallet([FromQuery] Guid walletId)
    {
        var userId = _userManager.GetUserId(User);

        var wallet = await _mediator.Send(new GetWalletRequest
        {
            UserId = userId,
            WalletId = walletId
        });

        return Ok(wallet);
    }

    [HttpGet("wallets")]
    public async Task<ActionResult<Wallet>> GetWallets()
    {
        var userId = _userManager.GetUserId(User);

        var wallets = await _mediator.Send(new GetInactiveWalletsRequest
        {
            UserId = userId
        });

        return Ok(wallets);
    }

    [HttpGet("wallets/active")]
    public async Task<ActionResult<Wallet>> GetActiveWallet()
    {
        var userId = _userManager.GetUserId(User);

        var wallet = await _mediator.Send(new GetActiveWalletRequest
        {
            UserId = userId
        });

        return Ok(wallet);
    }

    [HttpGet("wallets/d-active")]
    public async Task<ActionResult<Wallet>> GetActiveWalletDetails()
    {
        var userId = _userManager.GetUserId(User);

        var wallet = await _mediator.Send(new GetActiveWalletDetailRequest
        {
            UserId = userId
        });

        return Ok(wallet);
    }
}