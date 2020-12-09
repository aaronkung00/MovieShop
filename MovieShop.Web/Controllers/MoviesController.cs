using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieShop.Core.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShop.Web.Controllers
{
    public class MoviesController : Controller
    {

        private readonly ILogger<MoviesController> _logger;
        private readonly IMovieService _movieService;
        public MoviesController(ILogger<MoviesController> logger, IMovieService movieService)
        {
            _logger = logger;
            _movieService = movieService;
        }
        // Retrun Top 20 movies from DB
  
        public IActionResult Index()
        {

            return View();
        }
        // Retrun all movies in given genre
        public IActionResult MovieByGenre(int genreId)
        {

            return View();
        }

        // Retrun detail of movie with given movie id
        public async Task<IActionResult> Details(int id)
        {
            _logger.LogInformation("Details method called");
            var movieDetailsResponse= await _movieService.GetMovieAsync(id);
            return View(movieDetailsResponse);
        }
    }
}
