using dotnet_store.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace dotnet_store.Controllers;

public class KategoriController : Controller
{
    private readonly DataContext _context;

    public KategoriController(DataContext context)
    {
        _context = context;
    }
    public ActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Create(string kategoriAdi,string kategoriUrl)
    {
        var atama = new Kategori
        {
            KategoriAdi = kategoriAdi,
            Url = kategoriUrl
        };

        _context.Kategoriler.Add(atama);
        _context.SaveChanges();

        return RedirectToAction("Index");
    }
    public ActionResult Index()
    {
        //burada select yazmamıdaki sebep countunu veritabanından saydırmayıp aşırı yüklemeden kaçınmak
        var kategoriler = _context.Kategoriler.Select(i => new KategoriGetModels
        {
            Id = i.Id,
            KategoriAdi = i.KategoriAdi,
            Url = i.Url,
            UrunSayisi = i.Uruns.Count

        }).ToList();
        return View(kategoriler);
    }


}

