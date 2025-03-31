using dotnet_store.Models;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_store.Controllers;

public class AdminController : Controller
{
    private readonly DataContext db;
    public AdminController(DataContext context)
    {
        db = context;
    }
    public IActionResult Index()
    {
        ViewData["Urunler"] = db.Urunler.ToList();
        return View();
    }
}

