using Microsoft.AspNetCore.Mvc;
using MovieShop.Core.Models.Request_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShop.Web.Controllers
{

    public class AccountController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterRequestModel model)
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }
/*
        [HttpPost]
        public async Task<IActionResult> Login(UserRegisterModel model)
        {
            return View();
        }
*/
    }
}
