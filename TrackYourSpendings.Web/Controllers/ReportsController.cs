using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TrackYourSpendings.Web.Models;
using TrackYourSpendings.Web.Services;

namespace TrackYourSpendings.Web.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class ReportsController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IWalletService _walletService;

    public ReportsController(
        IWalletService walletService,
        UserManager<ApplicationUser> userManager)
    {
        _walletService = walletService;
        _userManager = userManager;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet("wallets/q")]
    public async Task<ActionResult<Wallet>> GetWallet([FromQuery] int walletId)
    {
        var userId = _userManager.GetUserId(User);

        var wallet = await _walletService.GetWalletDetails(walletId, userId);

        return Ok(wallet);
    }

    [HttpGet("wallets")]
    public async Task<ActionResult<Wallet>> GetWallets()
    {
        var userId = _userManager.GetUserId(User);
        var wallets = await _walletService.GetInactiveWallets(userId);

        return Ok(wallets);
    }
    
    [HttpGet("wallets/active")]
    public async Task<ActionResult<Wallet>> GetActiveWallet()
    {
        var userId = _userManager.GetUserId(User);

        var wallet = await _walletService.GetActiveWallet(userId);

        return Ok(wallet);
    }
    
    [HttpGet("wallets/d-active")]
    public async Task<ActionResult<Wallet>> GetActiveWalletDetails()
    {
        var userId = _userManager.GetUserId(User);

        var wallet = await _walletService.GetActiveWalletDetails(userId);

        return Ok(wallet);
    }
}