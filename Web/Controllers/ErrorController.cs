using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

public class ErrorController: Controller
{
    public IActionResult Error()
    {
        return View();
    }
}