using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieShop.Core.Exceptions;
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
    public class UserController : ControllerBase
    {

        private readonly IMovieService _movieService;
        private readonly IUserService _userService;

        public UserController(IUserService userService, IMovieService movieService)
        {
            _userService = userService;
            _movieService = movieService;
        }

      //  [Authorize]
        [HttpGet("{id:int}/purchases")]
        public async Task<ActionResult> GetUserPurchasedMoviesAsync(int id)
        {
            var userMovies = await _userService.GetAllPurchasesForUser(id);
            return Ok(userMovies);
        }

      //  [Authorize]
        [HttpGet("{id:int}/favorites")]
        public async Task<ActionResult> GetUserFavoriteMoviesAsync(int id)
        {
            var userMovies = await _userService.GetAllFavoritesForUser(id);
            return Ok(userMovies);
        }

      //  [Authorize]
        [HttpGet("{id:int}/reviews")]
        public async Task<ActionResult> GetUserReviewedMoviesAsync(int id)
        {
            var userMovies = await _userService.GetAllReviewsByUser(id);
            return Ok(userMovies);
        }

       // [Authorize]
        [HttpGet("{id:int}/movie/{movieId}/favorite")]
        public async Task<ActionResult> IsFavoriteExists(int id, int movieId)
        {
            var favoriteExists = await _userService.FavoriteExists(id, movieId);
            return Ok(new { isFavorited = favoriteExists });
        }

    //   [Authorize]
        [HttpDelete("{userId:int}/movie/{movieId:int}")]
        public async Task DeleteMovieReview(int userId, int movieId)
        {
            await _userService.DeleteMovieReview(userId, movieId);
        }
        
        //   [Authorize]
        [HttpPost("review")]
        public async Task<IActionResult> AddReview(ReviewRequestModel reviewRequest)
        {
            await _userService.AddMovieReview(reviewRequest);
            return Ok();
        }
        //[Authorize]
        [HttpPut("review")]
        public async Task<IActionResult> UpdateReview(ReviewRequestModel reviewRequest)
        {
            await _userService.UpdateReview(reviewRequest);
            return Ok();
        }

       // [Authorize]
        [HttpPost("favorite")]
        public async Task<IActionResult> AddFavorite(FavoriteRequestModel favoriteRequest)
        {
            await _userService.AddFavorite(favoriteRequest);
            return Ok("Add success!");
        }

        // [Authorize]
        [HttpPost("unfavorite")]
        public async Task<IActionResult> UnFavorite(FavoriteRequestModel favoriteRequest)
        {
            await _userService.RemoveFavorite(favoriteRequest);
            return Ok("unfavorite successed!");
        }
 

     //   [Authorize]
        [HttpPost("purchase")]
        public async Task<IActionResult> PurchaseMovie(PurchaseRequestModel purchaseRequest)
        {
          

            await _userService.PurchaseMovie(purchaseRequest);
            
            return Ok(purchaseRequest);
        }

    }
}
