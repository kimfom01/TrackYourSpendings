using Microsoft.AspNetCore.Mvc;

namespace TrackYourSpendings.Web.Controllers;

public class ErrorController: Controller
{
    public IActionResult Error()
    {
        return View();
    }
}