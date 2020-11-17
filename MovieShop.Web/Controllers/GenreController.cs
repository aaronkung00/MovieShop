using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShop.Web.Controllers
{
    public class GenreController : Controller
    {
        // Return All Genres
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
