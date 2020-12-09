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

            return Ok(createdMovie);
        }


        [HttpPut("movie")]
        public async Task<IActionResult> UpdateMovie(MovieCreateRequest movieCreateRequest)
        {
            var createdMovie = await _movieService.UpdateMovie(movieCreateRequest);

            return Ok(createdMovie);
        }

        // not yet implement 

        [HttpGet("purchases")]
        public async Task<IActionResult> GetAllPurchases([FromQuery] int pageSize = 30, [FromQuery] int page = 1)
        {
            var movies = await _movieService.GetAllMoviePurchasesByPagination(pageSize, page);
            return NotFound("Not yet implement");
        }

        [HttpGet("top")]
        public IActionResult GetTopMovies()
        {
            //   var movies = _cache.Get<IEnumerable<MovieChartResponseModel>>("chartsData");
            return NotFound("Not yet implement");
        }

        [HttpGet("push/{data}")]
        public async Task<IActionResult> PushNotification(string data)
        {
         //   await _hubContext.Clients.All.SendAsync("discountNotification", data);
            return NotFound("Not yet implement");
        }
    }
}
