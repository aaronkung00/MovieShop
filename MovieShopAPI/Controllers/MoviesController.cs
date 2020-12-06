using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieShop.Core.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShopAPI.Controllers
{

    // Attribute based routing
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }


        [HttpGet]
        [Route("toprevenue")]
        public async Task<IActionResult> GetTopRevenueMoviesAsync()
        {
            // call our service and call the method


            // return http status code
            var movies = await _movieService.GetHighestRevenueMovies();
            if (!movies.Any())
            {
                return NotFound("no Movies Found");
            }
            return Ok(movies);
        }
    }
}
