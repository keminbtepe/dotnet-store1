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
    public ActionResult Create(KategoriCreateModel model)
    {
        var atama = new Kategori
        {
            KategoriAdi = model.KategoriAdi,
            Url = model.Url
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

    public ActionResult Edit(int id)
    {
        var entity = _context.Kategoriler.Select(i => new KategoriEditModel
        {

            Id = i.Id,
            KategoriAdi = i.KategoriAdi,
            Url = i.Url

        }).FirstOrDefault(i => i.Id == id);

        return View(entity);
    }
    [HttpPost]
    public ActionResult Edit(int id, KategoriEditModel model)
    {
        if (id != model.Id)
        {
            return NotFound();
        }
        
        var entity = _context.Kategoriler.FirstOrDefault(i => i.Id == model.Id);

        if (entity != null)
        {
            entity.KategoriAdi = model.KategoriAdi;
            entity.Url = model.Url;

            _context.SaveChanges();
            return RedirectToAction("Index");

        }

        return View(model);


    }


}

