using Microsoft.AspNetCore.Mvc;

namespace Budget.Controllers;

public class ReportsController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}