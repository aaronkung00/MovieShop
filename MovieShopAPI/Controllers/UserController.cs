using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieShop.Core.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IMovieService _movieService;
        private readonly IUserService _userService;

        public UserController(IUserService userService, IMovieService movieService)
        {
            _userService = userService;
            _movieService = movieService;
        }

        [Authorize]
        [HttpGet("{id:int}/purchases")]
        public async Task<ActionResult> GetUserPurchasedMoviesAsync(int id)
        {
            var userMovies = await _userService.GetAllPurchasesForUser(id);
            return Ok(userMovies);
        }

        [Authorize]
        [HttpGet("{id:int}/favorites")]
        public async Task<ActionResult> GetUserFavoriteMoviesAsync(int id)
        {
            var userMovies = await _userService.GetAllFavoritesForUser(id);
            return Ok(userMovies);
        }

        [Authorize]
        [HttpGet("{id:int}/reviews")]
        public async Task<ActionResult> GetUserReviewedMoviesAsync(int id)
        {
            var userMovies = await _userService.GetAllReviewsByUser(id);
            return Ok(userMovies);
        }

    }
}
