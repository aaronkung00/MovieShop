using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieShop.Core.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;


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


        [HttpGet]
        [Route("{id:int}", Name = "GetMoviebyID")]
        public async Task<IActionResult> GetMovieByID(int id)
        {
            var movie = await _movieService.GetMovieAsync(id);
            return Ok(movie);
        }

        [HttpGet]
        [Route("GetTopRated")]
        public async Task<IActionResult> GetTopRated()
        {
            var movie = await _movieService.GetTopRatedMovies();
            return Ok(movie);
        }

        [HttpGet]
        [Route("genre/{genreId:int}")]
        public async Task<IActionResult> GetMoviesByGenre(int genreId)
        {
            var movie = await _movieService.GetMoviesByGenre(genreId);
            return Ok(movie);
        }

        [HttpGet]
        [Route("{id:int}/reviews")]
        public async Task<IActionResult> GetMovieReviews(int id)
        {
            var reviews = await _movieService.GetMovieReviews(id);
            return Ok(reviews);
        }

    }
}
