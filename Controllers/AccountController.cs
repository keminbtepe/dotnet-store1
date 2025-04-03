using dotnet_store.Models.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace dotnet_store.Controllers;

public class AccountController : Controller
{
    private UserManager<IdentityUser> _userManager;

    public AccountController(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
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
            var user = new IdentityUser
            {
                UserName = model.Username,
                Email = model.Email 
            };

            var result = await _userManager.CreateAsync(user, model.Password); //kullanıcıyı oluşturur ve şifreyi atar

            if (result.Succeeded)  //kullanıcı basarılı sekilde olusturulduysa
            {
                return RedirectToAction("Index","Home");
            }


            foreach (var error in result.Errors)   //hatanın detaylarını yazdırır 
            {
                ModelState.AddModelError("", error.Description);
            }

        }

        return View(model);
    }

}

