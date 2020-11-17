using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShop.Web.Controllers
{
    public class MoviesController : Controller
    {
        // Retrun Top 20 movies from DB
        [HttpGet]
        public IActionResult Index()
        {

            return View();
        }
        // Retrun all movies in given genre
        [HttpGet]
        public IActionResult MovieByGenre(int genreId)
        {
            
            return View();
        }

        // Retrun detail of movie with given movie id
        [HttpGet]
        public IActionResult Details(int movidId)
        {
            return View();
        }
    }
}
