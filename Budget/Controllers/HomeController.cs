using System.Diagnostics;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Budget.Models;
using Budget.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Budget.Controllers;

public class HomeController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public HomeController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
    }

    public IActionResult Index()
    {
        var wallets =  _unitOfWork.Wallets.GetWalletWithCategories();

        ViewBag.Wallets = new SelectList(wallets, "Id", "Name");
        
        ViewBag.Categories = new SelectList(wallets.First().Categories, "CategoryId", "CategoryName");

        return View(wallets);
    }

    [HttpPost]
    public async Task<IActionResult> Index(int id)
    {
        var wallet = await _unitOfWork.Wallets.GetEntity(id);

        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}