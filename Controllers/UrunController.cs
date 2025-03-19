using dotnet_store.Models;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_store.Controllers;

public class UrunController : Controller
{
    private readonly DataContext _context;
    public UrunController(DataContext context)
    {
        _context = context;
    }

    public ActionResult Index()
    {
        return View();
    }

    public ActionResult List(string url,string q)
    {
        var query = _context.Urunler.Where(i => i.Aktif).AsQueryable(); // bu komut üzerinden filtreleme yapmamýzý saðlar

        if (!string.IsNullOrEmpty(url))
        {
            //filtreleme
            query = query.Where(i => i.Kategori.Url == url);
        }
        if (!string.IsNullOrEmpty(q))
        {
            //filtreleme
            //Apple Watch 9 => apple
            query = query.Where(i => i.UrunAdi.ToLower().Contains(q.ToLower()));

            ViewData["q"] = q;

        }

        //var urunler = _context.Urunler.Where(i => i.Aktif && i.Kategori.Url == url).ToList();
        return View(query.ToList());
    }

    public ActionResult Details(int id)


    {
        // var urun = _context.Urunler.FirstOrDefault(i => i.Id == id);
        var urun = _context.Urunler.Find(id);

        if (urun == null)
        {
            return RedirectToAction("List");
        }

        ViewData["BenzerUrunler"] = _context.Urunler
            .Where(i => i.Aktif && i.KategoriId == urun.KategoriId && i.Id != id)
            .Take(4)
            .ToList();
        return View(urun);
    }
}