using dotnet_store.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

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
        ViewBag.Kategoriler = db.Kategoriler.ToList();
        return View();
    }

    [HttpPost]
    public async Task<ActionResult> Urunkaydet(UrunCreateModel model)
    {
        var filename = Path.GetRandomFileName() + ".jpeg";
        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", filename);

        using (var stream = new FileStream(path, FileMode.Create))
        {
            await model.Resim!.CopyToAsync(stream);
        }

        //if (model.ResimDosyasi != null)
        //{
        //    var fileName = Path.GetRandomFileName() + ".jpg";
        //    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", fileName);

        //    using (var stream = new FileStream(path, FileMode.Create))
        //    {
        //        await model.ResimDosyasi!.CopyToAsync(stream);
        //    }
        //    sorgu.Resim = fileName;

        //}

        var entity = new Urun()
        {

            UrunAdi = model.UrunAdi,
            Aciklama = model.Aciklama,
            Fiyat = model.Fiyat,
            Aktif = model.Aktif,
            KategoriId = model.KategoriId,
            Resim = filename  //upload control kullanýlacak

        };

        db.Urunler.Add(entity);
        db.SaveChanges();
        return RedirectToAction("Index");
    }
    public ActionResult UrunGuncelle(int id)
    {
        var sorgu = db.Urunler.Select(i => new UrunUpdateModel
        {
            Id = i.Id,
            UrunAdi = i.UrunAdi,
            Fiyat = i.Fiyat,
            ResimAdi = i.Resim,
            Aciklama = i.Aciklama,
            Aktif = i.Aktif,
            Anasayfa = i.Anasayfa,
            KategoriId = i.KategoriId


        }).FirstOrDefault(i => i.Id == id);

        ViewBag.Kategoriler = db.Kategoriler.ToList();



        return View(sorgu);
    }
    [HttpPost]
    public async Task<ActionResult> UrunGuncelle(int id, UrunUpdateModel model)
    {


        if (id != model.Id)
        {
            return RedirectToAction("Index");
        }

        var sorgu = db.Urunler.FirstOrDefault(i => i.Id == model.Id);
        var eskiurun = sorgu.UrunAdi;
        if (sorgu != null)
        {


            if (model.ResimDosyasi != null)
            {
                var fileName = Path.GetRandomFileName() + ".jpeg";
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await model.ResimDosyasi!.CopyToAsync(stream);
                }
                sorgu.Resim = fileName;

            }

            sorgu.UrunAdi = model.UrunAdi;
            sorgu.Fiyat = model.Fiyat;
            sorgu.Aciklama = model.Aciklama;
            sorgu.Aktif = model.Aktif;
            sorgu.Anasayfa = model.Anasayfa;
            sorgu.KategoriId = model.KategoriId;

            db.SaveChanges();

            TempData["Mesaj"] = $"{eskiurun} ürünü {sorgu.UrunAdi} olarak baþarýyla güncellendi";   //tempdata farklý actionlarda kullanýlabilir


            return RedirectToAction("Index");

        }

        return View(model);
    }

    public ActionResult UrunSil(int? id)
    {
        if (id == null)
        {
            return RedirectToAction("Index");
        }

        var entity = db.Urunler.Find(id);
        var silinen  = entity.UrunAdi;

        if (entity != null)
        {
            db.Urunler.Remove(entity);
            db.SaveChanges();

            TempData["Mesaj"] = $"{silinen} Ürünü baþarýyla silindi";
        }

        return RedirectToAction("Index");
    }

}

