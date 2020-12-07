using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieShop.Core.Models.Request_Model;
using MovieShop.Core.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public AdminController(IMovieService movieService)
        {
            _movieService = movieService; 

        }

        [HttpPost("movie")]
        public async Task<IActionResult> CreateMovie(MovieCreateRequest movieCreateRequest)
        {
            var createdMovie = await _movieService.CreateMovie(movieCreateRequest);

            return Ok("Success!");
        }

    }
}
