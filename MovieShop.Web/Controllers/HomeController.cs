using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieShop.Core.ServiceInterfaces;
using MovieShop.Web.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShop.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMovieService _movieService;
        public HomeController(ILogger<HomeController> logger, IMovieService movieService)
        {
            _logger = logger;
            _movieService = movieService;
        }

        public async Task<IActionResult> Index()
        {
          //  var movies = await _movieService.GetHighestRevenueMovies();
            var movies = await _movieService.GetMovieAsync(1);
            return View(movies);

            // By default when you do return View its gonna
            // return View with same name as action method
            // name inside View folder of that controller name folder
            // you can specify name in view("your file")
            // otherwise it will search the name of signature

            // HttpContext in ASP.NET and ASP.NET which will provide
            // you with all the info regarding your http request


            // controllers will call Services  ==> Repositories
            // Navigation => list of Genres as a dropdown
            // showing top 20 highest rev
            // card  in bootstrap, cardimage, movieid, title
            // movie entity has all properties but we dont need that many
            // So we create a model based on our UI/API requirements.

            // Models/ViewModels in MVC
            // DTO - data transfer objects in API. We create custom classes based on our UI/API req



        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
