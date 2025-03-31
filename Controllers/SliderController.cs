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
        var sliders = db.Sliderlar.Select(i => new SliderGetModel
        {
            Id = i.Id,
            Resim = i.Resim,
            Baslik = i.Baslik,
            Aktif = i.Aktif,
             Index = i.Index


        }).ToList();
            
        
        return View(sliders);
    }
}

