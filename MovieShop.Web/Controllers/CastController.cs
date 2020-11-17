using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShop.Web.Controllers
{
    public class CastController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        // Return a Cast details with given id 
        public IActionResult Details(int castId)
        {
            return View();
        }
    }
}
