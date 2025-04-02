using dotnet_store.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;

namespace dotnet_store.Controllers;

public class SliderController : Controller
{
    private readonly DataContext db;
    public SliderController(DataContext context)
    {
        db = context;
    }
    public ActionResult Index()
    {
        var sliders = db.Sliderlar.Select(i => new SliderGetModell
        {
            Id = i.Id,
            Resim = i.Resim,
            Baslik = i.Baslik,
            Aktif = i.Aktif,
            Index = i.Index


        }).OrderBy(i => i.Index).ToList();


        return View(sliders);
    }

    #region EKLEME
    public ActionResult Create()
    {

        return View();
    }

    [HttpPost]
    public async Task<ActionResult> CreateAsync(SliderCreateModel model)
    {
        if (ModelState.IsValid)
        {
            var filename = Path.GetRandomFileName() + ".jpeg";
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", filename);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await model.Resim!.CopyToAsync(stream);
            }


            var entity = new Slider
            {
                Resim = filename,
                Baslik = model.Baslik,
                Aktif = model.Aktif,
                Index = model.Index,
                Aciklama = model.Aciklama

            };

            db.Sliderlar.Add(entity);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
        return View();
    }
    #endregion



    #region GÜNCELLEME
    public ActionResult Edit(int id)
    {
        //textboxlara yazdırma kodları
        var sorgu = db.Sliderlar.Select(i => new SliderEditModel
        {
            Aciklama = i.Aciklama,
            Baslik = i.Baslik,
            Aktif = i.Aktif,
            Index = i.Index,
            ResimAdi = i.Resim,
            Id = i.Id

        }
        ).FirstOrDefault(i => i.Id == id);

        return View(sorgu);
    }
    [HttpPost]
    public async Task<ActionResult> Edit(int id, SliderEditModel model)
    {
        if (id != model.Id)
        {
            return RedirectToAction("Index");
        }

        var sorgu = db.Sliderlar.FirstOrDefault(i => i.Id == id);
        var eskiSlider = model.Baslik;

        if (sorgu != null)
        {

            if (sorgu != null)
            {
                if (model.Resim != null)
                {
                    var filename = Path.GetRandomFileName() + ".jpeg";
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", filename);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await model.Resim!.CopyToAsync(stream);
                    }
                    sorgu.Resim = filename;
                }

                sorgu.Baslik = model.Baslik;
                sorgu.Aciklama = model.Aciklama;
                sorgu.Aktif = model.Aktif;
                sorgu.Index = model.Index;

                db.SaveChanges();

                TempData["Mesaj"] = $"{eskiSlider} Slideri {sorgu.Baslik} olarak başarıyla güncellendi";
                return RedirectToAction("Index");
            }
        }
        return View();
    }
    #endregion



    #region SİLME
    public ActionResult Delete(int? id)
    {
        if (id == null)
        {
            return RedirectToAction("Index");
        }

        var sorgu = db.Sliderlar.Find(id);
        var silinen = sorgu.Baslik;

        if (sorgu != null)
        {
            db.Sliderlar.Remove(sorgu);
            db.SaveChanges();

            TempData["Mesaj"] = $"{silinen} Ürünü başarıyla silindi";
        }

        return RedirectToAction("Index");
    }
    #endregion
}



