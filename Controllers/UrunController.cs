using dotnet_store.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dotnet_store.Controllers;

public class UrunController : Controller
{
    private readonly DataContext db;
    public UrunController(DataContext context)
    {
        db = context;
    }

    public ActionResult Index()
    {
        var urunListesi = db.Urunler
            .Include(i => i.Kategori)
            .ToList();

        return View(urunListesi);


        //var urunler = _context.Urunler.Select(i => new UrunGetModel
        //{
        //    Id = i.Id,
        //    UrunAdi = i.UrunAdi,
        //    Fiyat = i.Fiyat,
        //    Resim = i.Resim,
        //    Aktif = i.Aktif,
        //    Anasayfa = i.Anasayfa,
        //    KategoriAdi = i.Kategori.KategoriAdi

        //}).ToList();

        //return View(urunler);
    }

    public ActionResult List(string url, string q)
    {
        var query = db.Urunler.Where(i => i.Aktif).AsQueryable(); // bu komut üzerinden filtreleme yapmamýzý saðlar

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
        var urun = db.Urunler.Find(id);

        if (urun == null)
        {
            return RedirectToAction("List");
        }

        ViewData["BenzerUrunler"] = db.Urunler
            .Where(i => i.Aktif && i.KategoriId == urun.KategoriId && i.Id != id)
            .Take(4)
            .ToList();
        return View(urun);
    }

    public ActionResult Urunkaydet()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Urunkaydet(int a)
    {
        return View();
    }
}

