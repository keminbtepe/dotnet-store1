using dotnet_store.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_store.Controllers;

[Authorize]
public class AdminController : Controller
{
    private readonly DataContext db;
    public AdminController(DataContext context)
    {
        db = context;
    }
    public IActionResult Index()
    {
        var model = new AdminIndexViewModel();
        model.Urunler = db.Urunler.ToList();
        model.Kategoriler = db.Kategoriler.ToList();
        model.Mesaj = "Hoşgeldiniz";

        ViewBag.Mesaj = "bu bir viewbagdan gelen mesaj";
        ViewBag.Urunler = db.Urunler.ToList();

        return View(model);
    }
}

