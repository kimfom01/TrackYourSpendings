using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Google;

namespace Web.Controllers;

[Authorize]
public class UserController : Controller
{
    [AllowAnonymous]
    public async Task<IActionResult> SignIn()
    {
        var accessToken = await HttpContext.GetTokenAsync(GoogleDefaults.AuthenticationScheme, "access_token");
        Console.WriteLine(accessToken);

        return RedirectToAction(nameof(HomeController.Index), "Home");
    }

    [HttpGet]
    public async Task<IActionResult> SignOutUser()
    {
        await HttpContext.SignOutAsync();
        return RedirectToAction(nameof(HomeController.Index), "Home");
    }
}
