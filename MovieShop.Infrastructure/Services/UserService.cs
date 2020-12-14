using MovieShop.Core.Entities;
using MovieShop.Core.Exceptions;
using MovieShop.Core.Models;
using MovieShop.Core.Models.Request_Model;
using MovieShop.Core.Models.Response_Model;
using MovieShop.Core.RepositoryInterfaces;
using MovieShop.Core.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static MovieShop.Core.Models.Response_Model.FavoriteResponseModel;
using static MovieShop.Core.Models.Response_Model.PurchaseResponseModel;

namespace MovieShop.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IAsyncRepository<UserRole> _userRoleRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICryptoService _encryptionService;
        private readonly IMovieService _movieService;
        private readonly IAsyncRepository<Purchase> _purchaseRepository;
        private readonly IAsyncRepository<Favorite> _favoriteRepository;
        private readonly IAsyncRepository<Review> _reviewRepository;
      

        public UserService(ICryptoService encryptionService, IUserRepository userRepository, IAsyncRepository<Favorite> favRepository,
            IAsyncRepository<Purchase> purchaseRepository, IAsyncRepository<Review> reviewRepository, IMovieService movieService)
        {
            _encryptionService = encryptionService;
            _userRepository = userRepository;
            _favoriteRepository = favRepository;
            _purchaseRepository = purchaseRepository;
            _reviewRepository = reviewRepository;
            _movieService = movieService;
        }



        public async Task<UserLoginResponseModel> ValidateUser(string email, string password)
        {
            // we are gonna check if the email exists in the database
            var user = await _userRepository.GetUserByEmail(email);
            if (user == null) return null;


            var hashedPassword = _encryptionService.HashPassword(password, user.Salt);
            var isSuccess = user.HashedPassword == hashedPassword;

            /*    var roles = await _userRoleRepository.ListAllWithIncludesAsync(ur => ur.UserId == user.Id, role => role.Role);

                var response = _mapper.Map<UserLoginResponseModel>(user);

                var userRoles = roles.ToList();
                if (userRoles.Any())
                {
                    response.Roles = userRoles.Select(r => r.Role.Name).ToList();
                }

            */

            var response = new UserLoginResponseModel { 
                Id = user.Id, Email = user.Email,
                LastName = user.LastName,
                FirstName = user.FirstName,
                DateOfBirth = user.DateOfBirth,             
            };

            return isSuccess ? response : null;

        }
        
        
        public async Task<UserRegisterResponseModel> CreateUser(UserRegisterRequestModel requestModel)
        {
            var dbUser = await _userRepository.GetUserByEmail(requestModel.Email);
            if (dbUser != null && string.Equals(dbUser.Email, requestModel.Email, StringComparison.CurrentCultureIgnoreCase))
                throw new Exception("Email Already Exits");

            var salt = _encryptionService.CreateSalt();
            var hashedPassword = _encryptionService.HashPassword(requestModel.Password, salt);
            var user = new User
            {
                Email = requestModel.Email,
                Salt = salt,
                HashedPassword = hashedPassword,
                FirstName = requestModel.FirstName,
                LastName = requestModel.LastName
            };
            var createdUser = await _userRepository.AddAsync(user);

            var response = new UserRegisterResponseModel
            {
                Id = createdUser.Id, Email = createdUser.Email, FirstName = createdUser.FirstName,
                LastName = createdUser.LastName
            };
            //var response = _mapper.Map<UserRegisterResponseModel>(createdUser);
            return response;
        }

        public async Task<UserRegisterResponseModel> GetUserDetails(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
           
            var response = new UserRegisterResponseModel
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
            return response;
        }

        public async Task<bool> FavoriteExists(int id, int movieId)
        {
            return await _favoriteRepository.GetExistsAsync(f => f.MovieId == movieId &&
                                                                 f.UserId == id);
        }

        public async Task<FavoriteResponseModel> GetAllFavoritesForUser(int id)
        {
            /*if (_currentUserService.UserId != id)
                throw new HttpException(HttpStatusCode.Unauthorized, "You are not Authorized to View Favorites");
            //todo
            */

            var favoriteMovies = await _favoriteRepository.ListAllWithIncludesAsync(
               p => p.UserId == id,
               p => p.Movie);

            var response = new FavoriteResponseModel
            {
                FavoriteMovies = new List<FavoriteMovieResponseModel>()
            };

            foreach (var f_mv in favoriteMovies )
            {
                if (f_mv.Movie.ReleaseDate != null)
                {
                    response.FavoriteMovies.Add(new FavoriteMovieResponseModel
                    {
                        Id = f_mv.Movie.Id,
                        Title = f_mv.Movie.Title,
                        PosterUrl = f_mv.Movie.PosterUrl,
                        ReleaseDate = f_mv.Movie.ReleaseDate.Value
                    });
                }
            }


            return response;
        }

        public async Task<ReviewResponseModel> GetAllReviewsByUser(int id)
        {
            /*
             * if (_currentUserService.UserId != id)
                    throw new HttpException(HttpStatusCode.Unauthorized, "You are not Authorized to View Favorites"); 
            */



            var reviewsByUser = await _reviewRepository.ListAllWithIncludesAsync(e => e.UserId == id, e => e.Movie);

            var response = new ReviewResponseModel {
                UserId = id,
                MovieReviews = new List<ReviewMovieResponseModel>()
            };

            foreach (var rv in reviewsByUser)
            {
                response.MovieReviews.Add(
                    new ReviewMovieResponseModel
                    {
                        MovieId = rv.MovieId,
                        UserId = rv.UserId,
                        Name = rv.Movie.Title,
                        ReviewText = rv.ReviewText,
                        Rating = rv.Rating
                    });
            }

            return response;
        }

        public async Task<PurchaseResponseModel> GetAllPurchasesForUser(int id)
        {/*
            if (_currentUserService.UserId != id)
                throw new HttpException(HttpStatusCode.Unauthorized, "You are not Authorized to View Purchases");
            */
            var purchasedMovies = await _purchaseRepository.ListAllWithIncludesAsync(
                // p => p.UserId == _currentUserService.UserId,
                p => p.UserId == id,
                p => p.Movie);

            var response = new PurchaseResponseModel
            {
                UserId = id,
                PurchasedMovies = new List<PurchasedMovieResponseModel>()
            };

            foreach (var p_movie in purchasedMovies)
            {
                response.PurchasedMovies.Add(new PurchasedMovieResponseModel
                {

                    Id = p_movie.Id,
                    Title = p_movie.Movie.Title,
                    PosterUrl = p_movie.Movie.PosterUrl,
                    PurchaseDateTime = p_movie.PurchaseDateTime.Value,
                    ReleaseDate =  p_movie.Movie.ReleaseDate.Value
                }) ;
            }

            return response;
        }

        public async Task DeleteMovieReview(int userId, int movieId)
        {
            /*
            if (_currentUserService.UserId != id)
                throw new HttpException(HttpStatusCode.Unauthorized, "You are not Authorized to View Purchases");
            */

            var review = await _reviewRepository.ListAsync(r => r.UserId == userId && r.MovieId == movieId);
            await _reviewRepository.DeleteAsync(review.First());
        }

        public async Task AddMovieReview(ReviewRequestModel reviewRequest)
        {
            /*
               if (_currentUserService.UserId != id)
                   throw new HttpException(HttpStatusCode.Unauthorized, "You are not Authorized to View Purchases");
               */
            var review = new Review { 
              
                UserId = reviewRequest.UserId,
                MovieId = reviewRequest.MovieId,
                ReviewText = reviewRequest.reviewText,
                Rating = reviewRequest.Rating
       
            };
            await _reviewRepository.AddAsync(review);
        }

        public async Task UpdateReview(ReviewRequestModel reviewRequest)
        {
            /*
           if (_currentUserService.UserId != id)
               throw new HttpException(HttpStatusCode.Unauthorized, "You are not Authorized to View Purchases");
           */
            var review = new Review
            {

                UserId = reviewRequest.UserId,
                MovieId = reviewRequest.MovieId,
                ReviewText = reviewRequest.reviewText,
                Rating = reviewRequest.Rating

            };
            await _reviewRepository.UpdateAsync(review);
        }

        public async Task AddFavorite(FavoriteRequestModel favoriteRequest)
        {
            /*
                if (_currentUserService.UserId != id)
                    throw new HttpException(HttpStatusCode.Unauthorized, "You are not Authorized to View Purchases");
            */
            /*
                        var temp = await _favoriteRepository.GetExistsAsync(f => f.MovieId == favoriteRequest.MovieId &&
                                                                            f.UserId == favoriteRequest.UserId);
            */
          
            if (await FavoriteExists(favoriteRequest.UserId, favoriteRequest.MovieId))
               throw new ConflictException("Movie already Favorited");

            var fav_ef = new Favorite {
               MovieId = favoriteRequest.MovieId,
               UserId = favoriteRequest.UserId,
            };

            await _favoriteRepository.AddAsync(fav_ef);
        }

        public async Task RemoveFavorite(FavoriteRequestModel favoriteRequest)
        {
            var dbFav = await _favoriteRepository.ListAsync(r => r.UserId == favoriteRequest.UserId && r.MovieId == favoriteRequest.MovieId);
            await _favoriteRepository.DeleteAsync(dbFav.First());
        }

        public async Task<bool> IsMoviePurchased(PurchaseRequestModel purchaseRequest)
        {
            return await _purchaseRepository.GetExistsAsync(p =>
                p.UserId == purchaseRequest.UserId && p.MovieId == purchaseRequest.MovieId);
        }

        public async Task PurchaseMovie(PurchaseRequestModel purchaseRequest)
        {   /*
            if (_currentUserService.UserId != purchaseRequest.UserId)
                throw new HttpException(HttpStatusCode.Unauthorized, "You are not Authorized to purchase");
            if (_currentUserService.UserId != null) purchaseRequest.UserId = _currentUserService.UserId.Value;
            */

            if (await IsMoviePurchased(purchaseRequest))
                throw new ConflictException("The Movie has been purchased.");

            var movie = await _movieService.GetMovieAsync(purchaseRequest.MovieId);
            purchaseRequest.TotalPrice = movie.Price;
            var purchase = new Purchase {
                UserId = purchaseRequest.UserId,
                MovieId = purchaseRequest.MovieId,
                TotalPrice = purchaseRequest.TotalPrice,
                PurchaseNumber = new Guid(),
                PurchaseDateTime = DateTime.Now
            };
            await _purchaseRepository.AddAsync(purchase);
        }
    }
}
