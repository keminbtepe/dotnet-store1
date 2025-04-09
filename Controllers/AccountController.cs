using dotnet_store.Models;
using dotnet_store.Models.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Sqlite.Storage.Json.Internal;
using System.Threading.Tasks;

namespace dotnet_store.Controllers;

public class AccountController : Controller
{
    private UserManager<AppUser> _userManager;

    private SignInManager<AppUser> _signInManager;

    public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }
    public IActionResult Create()
    {
        return View();
    }


    [HttpPost]
    public async Task<IActionResult> Create(AccountCreateModel model)
    {

        if (ModelState.IsValid)
        {
            var user = new AppUser
            {
                UserName = model.Email,
                Email = model.Email,
                AdSoyad = model.AdSoyad
            };

            var result = await _userManager.CreateAsync(user, model.Password); //kullanıcıyı oluşturur ve şifreyi atar

            if (result.Succeeded)  //kullanıcı basarılı sekilde olusturulduysa
            {
                return RedirectToAction("Index", "Home");
            }


            foreach (var error in result.Errors)   //hatanın detaylarını yazdırır 
            {
                ModelState.AddModelError("", error.Description);
            }

        }

        return View(model);
    }


    //LOGİN İŞLEMİ
    public ActionResult Login()
    {
        return View();
    }


    [HttpPost]
    public async Task<IActionResult> Login(AccountLoginModel model, string? returnurl)  //string returnUrl çalışmadıgı için sildim
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByEmailAsync(model.Email); //emaili bulur
            if (user != null)
            {
                await _signInManager.SignOutAsync();  //daha önce giriş yaptıysa o cookieyi siler

                var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.BeniHatırla, false); //şifreyi kontrol eder

                if (result.Succeeded)
                {

                    if (!string.IsNullOrEmpty(returnurl))
                    {
                        return Redirect(returnurl);
                    }

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Parola Hatalı");
                }
            }
            ModelState.AddModelError("", "Kullanıcı adı hatalı");
        }
        return View(model);
    }
    // LOGİN İŞLEMİ 


    public async Task<ActionResult> LogOut()
    {
        await _signInManager.SignOutAsync(); //çıkış yapar
        return RedirectToAction("Login", "Account");
    }

    [Authorize]
    public ActionResult Settings()
    {
        return View();
    }
}

