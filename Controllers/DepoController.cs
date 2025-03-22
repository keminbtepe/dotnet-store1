using dotnet_store.Migrations;
using dotnet_store.Models;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_store.Controllers;

public class DepoController : Controller
{
    #region ctor
    private readonly DataContext db;

    public DepoController(DataContext context)
    {
        db = context;
    }
    #endregion 
     


    #region Views // Sayfalar
    public IActionResult Index() // Depoların Anasayfası (Tam sayfa)
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> List() // Depoların Listesinin olduğu parçalı sayfa (Parçalı görünüm yani layout'suz)
    {
        try
        { 
            List<Depo> depolar = db.Depolar.ToList();

            return PartialView(depolar);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpPost]
    public async Task<IActionResult> Form(int id) // Depo Ekleme ve Güncelleme formunun olduğu sayfa (Parçalı görünüm yani layout'suz)
    {
        Depo depo = new Depo();
        depo.Aktifmi = true;

        if (id > 0) 
            depo = db.Depolar.Find(id); 

        return PartialView(depo);
    }
    #endregion



    #region Tasks // İşlemler
    [HttpPost]
    public async Task<IActionResult> Save(Depo form) // Depo Ekleme veya Güncelleme işlemi
    {
        try
        {
            if (form.DepoId > 0) // Id 0 dan büyükse güncelle
            {
                Depo depo = db.Depolar.Find(form.DepoId); // Depoyu veritabanından çek

                depo.Adi = form.Adi;
                depo.Adres = form.Adres;
                depo.Aktifmi = form.Aktifmi;
                depo.YoneticiAdi = form.YoneticiAdi;

                db.Depolar.Update(depo); // Depoyu veritabanında güncelle komutu ver
                db.SaveChanges();
            }
            else // Id 0 ise yeni kayıt
            {
                db.Depolar.Add(form); // Depoyu yeni kayıt olarak ekle
                db.SaveChanges();
            }

            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int id) // Depo Silme işlemi
    {
        try
        {
            if (id == 0) 
                return BadRequest("0 id li kayıt olamaz");

            Depo depo = db.Depolar.Find(id); // Gelen id ile eşleşen Depoyu veritabanından çek

            db.Depolar.Remove(depo); // Çekilen depoyu sil komutu

            db.SaveChanges(); // Veritabanı değişikliklerini kaydet

            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    #endregion
}