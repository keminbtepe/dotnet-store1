using Microsoft.AspNetCore.Mvc;

namespace dotnet_store.Controllers;

    public class AccountController : Controller
    {
        public IActionResult Create()
        {
            return View();
        }
    }

