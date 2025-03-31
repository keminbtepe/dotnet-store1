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
        if (ModelState.IsValid)
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
        return View(model);
    }
    public ActionResult Index()
    {
        //burada select yazmamıdaki sebep countunu veritabanından saydırmayıp aşırı yüklemeden kaçınmak
        var kategoriler = _context.Kategoriler.Select(i => new KategoriGetModel
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

        if (ModelState.IsValid)  //eror mesajı koşulları saglanırsa içerisi çalışır yoksa çalışmaz
        {



            var entity = _context.Kategoriler.FirstOrDefault(i => i.Id == model.Id);
            var eskikategori = entity.KategoriAdi;

            if (entity != null)
            {
                entity.KategoriAdi = model.KategoriAdi;
                entity.Url = model.Url;


                _context.SaveChanges();

                TempData["Mesaj"] = $"{eskikategori} Kategorisi {entity.KategoriAdi} olarak başarıyla güncellendi";   //tempdata farklı actionlarda kullanılabilir

                return RedirectToAction("Index");

            }
        }
        return View(model);


    }

    public ActionResult Delete(int? id)
    {

        if (id == null) 
        {
            return RedirectToAction("Index");
        }

        var sorgu = _context.Kategoriler.Find(id);
        var eski = sorgu.KategoriAdi;
        if (sorgu != null) 
        {
            _context.Kategoriler.Remove(sorgu);
            _context.SaveChanges();

            TempData["Mesaj"] = $"{eski} Kategorisi başarıyla silindi";   //tempdata farklı actionlarda kullanılabilir

        }
        return RedirectToAction("Index");
    }


}

