using MovieShop.Core.Entities;
using MovieShop.Core.Helpers;
using MovieShop.Core.Models;
using MovieShop.Core.Models.Request_Model;
using MovieShop.Core.RepositoryInterfaces;
using MovieShop.Core.ServiceInterfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using static MovieShop.Core.Models.MovieDetailsResponseModel;

namespace MovieShop.Infrastructure.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IAsyncRepository<MovieGenre> _genresRepository;
        private readonly IPurchaseRepository _purchaseRepository;

        // Constructor Injection
        // DI is pattern that enables us to write loosely coupled code so that the code 
        public MovieService(IMovieRepository movieRepository, IAsyncRepository<MovieGenre> genresRepository, IPurchaseRepository purchaseRepository)
        {
            // create MovieRepo instance in every method in my service class
            // newing up is very convineint but we need to avoid it as much as we can
            // make sure you dont break any existing code....
            // always go back do the regression testing...
            //  _repository = new MovieRepository(new MovieShopDbContext(null));
            _movieRepository = movieRepository;
            _genresRepository = genresRepository;
            _purchaseRepository = purchaseRepository;

        }

        

        public async Task<IEnumerable<MovieResponseModel>> GetHighestRevenueMovies()
        {
            // Repository?
            // MovieRepository class
            // var repository = new MovieRepository(new MovieShopDbContext(null));
            var movies = await _movieRepository.GetHighestRevenueMovies();
            // Map our Movie Entity to MovieResponseModel
            var movieResponseModel = new List<MovieResponseModel>();
            foreach (var movie in movies)
            {
                movieResponseModel.Add(new MovieResponseModel
                {
                    Id = movie.Id,
                    PosterUrl = movie.PosterUrl,
                   // ReleaseDate = movie.ReleaseDate.Value,
                    Title = movie.Title
                });
            }
            return movieResponseModel;
        }

        public async Task<MovieDetailsResponseModel> GetMovieAsync(int id)
        {
            var movie = await _movieRepository.GetByIdAsync(id);
            var movieDetailsResponseModel = new MovieDetailsResponseModel
            {
                
                Id = movie.Id,
                Title = movie.Title,
                PosterUrl = movie.PosterUrl,
                BackdropUrl = movie.BackdropUrl,
                // Movie - not have rating now
                // Rating = null,
                Overview = movie.Overview,
                Tagline = movie.Tagline,
                Budget = movie.Budget,
                Revenue = movie.Revenue,
                ImdbUrl = movie.ImdbUrl,
                TmdbUrl = movie.TmdbUrl,
                ReleaseDate = movie.ReleaseDate,
                RunTime = movie.RunTime,
                Price = movie.Price,
                Casts = new List<CastResponseModel>(),
                Genres = new List<Genre>()
                
                // FavoritesCount = movie.FavoritesCount;

            };

            foreach (var movieCast in movie.MovieCasts)
            {
                
                movieDetailsResponseModel.Casts.Add(new CastResponseModel { 
                    
                    Id = movieCast.Cast.Id,
                    Name = movieCast.Cast.Name,
                    Gender = movieCast.Cast.Gender,
                    Character = movieCast.Character,
                    ProfilePath = movieCast.Cast.ProfilePath,
                    TmdbUrl = movieCast.Cast.TmdbUrl
                   
                });
            }

            foreach (var genre in movie.MovieGenres)
            {
                movieDetailsResponseModel.Genres.Add(genre.Genre);
            }


            return movieDetailsResponseModel;
        }

        public async Task<IEnumerable<Review>> GetMovieReviews(int id)
        {
            var reviewsByMovieId = await _movieRepository.GetMovieReviews(id);

            return reviewsByMovieId;
        }

        public async Task<IEnumerable<MovieResponseModel>> GetMoviesByGenre(int genreId)
        {
            var moviesByGenre = await _movieRepository.GetMoviesByGenre(genreId);
            var movieByGenreResponseModels = new List<MovieResponseModel>();
            foreach (var mv in moviesByGenre)
            {
                if (mv.ReleaseDate != null)
                {
                    movieByGenreResponseModels.Add(
                     new MovieResponseModel
                     {
                         Id = mv.Id,
                         Title = mv.Title,
                         PosterUrl = mv.PosterUrl,
                         ReleaseDate = mv.ReleaseDate.Value
                     }
                    );
                }
            }
            return movieByGenreResponseModels;
        }

        public async Task<IEnumerable<MovieResponseModel>> GetTopRatedMovies()
        {
            var topmovies = await _movieRepository.GetTopRatedMovies();
            var movieTopRatedResponseModels = new List<MovieResponseModel>();
            foreach (var mv in topmovies)
            {
                if (mv.ReleaseDate != null)
                {
                    movieTopRatedResponseModels.Add(
                     new MovieResponseModel
                         {
                             Id = mv.Id,
                             Title = mv.Title,
                             PosterUrl = mv.PosterUrl,
                             ReleaseDate = mv.ReleaseDate.Value
                         }
                    );
                }
         
            }
            return movieTopRatedResponseModels;
        }

        public async Task<MovieDetailsResponseModel> CreateMovie(MovieCreateRequest movieCreateRequest)
        {

            var movie = new Movie
            {

                Title = movieCreateRequest.Title,
                Overview = movieCreateRequest.Overview,
                Tagline = movieCreateRequest.Tagline,
                ImdbUrl = movieCreateRequest.ImdbUrl,
                TmdbUrl = movieCreateRequest.TmdbUrl,
                BackdropUrl = movieCreateRequest.BackdropUrl,
                PosterUrl = movieCreateRequest.PosterUrl,
                Budget = movieCreateRequest.Budget,
                RunTime = movieCreateRequest.RunTime,
                ReleaseDate = movieCreateRequest.ReleaseDate,
                Revenue = movieCreateRequest.Revenue,
                Price = movieCreateRequest.Price,
                OriginalLanguage = movieCreateRequest.OriginalLanguage
            };


            var createdMovieDetail = await _movieRepository.AddAsync(movie);

            if (movieCreateRequest.Genres != null)
            {
                foreach (var genre in movieCreateRequest.Genres)
                {
                    var movieGenre = new MovieGenre { MovieId = createdMovieDetail.Id, GenreId = genre.Id };
                    await _genresRepository.AddAsync(movieGenre);
                }
            }


            var respose = new MovieDetailsResponseModel
            {
                Id = createdMovieDetail.Id,
                Title = createdMovieDetail.Title,
                Overview = createdMovieDetail.Overview,
                Tagline = createdMovieDetail.Tagline,
                ImdbUrl = createdMovieDetail.ImdbUrl,
                TmdbUrl = createdMovieDetail.TmdbUrl,
                BackdropUrl = createdMovieDetail.BackdropUrl,
                PosterUrl = createdMovieDetail.PosterUrl,
                Budget = createdMovieDetail.Budget,
                RunTime = createdMovieDetail.RunTime,
                ReleaseDate = createdMovieDetail.ReleaseDate,
                Revenue = createdMovieDetail.Revenue,
                Price = createdMovieDetail.Price,
                OriginalLanguage = createdMovieDetail.OriginalLanguage
            };

            return respose;
        }

        public async Task<MovieDetailsResponseModel> UpdateMovie(MovieCreateRequest movieCreateRequest)
        {
            
            var movie = new Movie
            {
                Id = movieCreateRequest.Id,
                Title = movieCreateRequest.Title,
                Overview = movieCreateRequest.Overview,
                BackdropUrl = movieCreateRequest.BackdropUrl,
                Budget = movieCreateRequest.Budget,
                Revenue = movieCreateRequest.Revenue,
                Tagline = movieCreateRequest.Tagline,
                PosterUrl = movieCreateRequest.PosterUrl,
                TmdbUrl = movieCreateRequest.TmdbUrl,
                ImdbUrl = movieCreateRequest.ImdbUrl,
                ReleaseDate = movieCreateRequest.ReleaseDate,
                OriginalLanguage = movieCreateRequest.OriginalLanguage,
                RunTime = movieCreateRequest.RunTime,
                Price = movieCreateRequest.Price,
            };


            var updateMovie = await _movieRepository.UpdateAsync(movie);
            if(movieCreateRequest.Genres != null)
            {
                     foreach (var genre in movieCreateRequest.Genres)
                        {
                            var movieGenre = new MovieGenre { MovieId = updateMovie.Id, GenreId = genre.Id };
                            await _genresRepository.UpdateAsync(movieGenre);
                        }
            }
       


            var response = new MovieDetailsResponseModel 
            {
                Id = updateMovie.Id,
                Title = updateMovie.Title,
                Overview = updateMovie.Overview,
                Tagline = updateMovie.Tagline,
                ImdbUrl = updateMovie.ImdbUrl,
                TmdbUrl = updateMovie.TmdbUrl,
                BackdropUrl = updateMovie.BackdropUrl,
                PosterUrl = updateMovie.PosterUrl,
                Budget = updateMovie.Budget,
                RunTime = updateMovie.RunTime,
                ReleaseDate = updateMovie.ReleaseDate,
                Revenue = updateMovie.Revenue,
                Price = updateMovie.Price,
                OriginalLanguage = updateMovie.OriginalLanguage

            };

            return response; 
        }

        public async Task<PagedResultSet<MovieResponseModel>> GetMoviesByPagination(int pageSize = 20, int page = 0, string title = "")
        {
            
            throw new System.NotImplementedException();
        }

        public async Task<PagedResultSet<MovieResponseModel>> GetAllMoviePurchasesByPagination(int pageSize = 20, int page = 0)
        {
            var purchaseCount = await _purchaseRepository.GetCountAsync();
            var purchases = await _purchaseRepository.GetAllPurchases(pageSize, page);
            var data = new List<MovieResponseModel>();
            if (purchases != null)
            {
                foreach (var purchase in purchases)
                {
                    data.Add(new MovieResponseModel { 
                        Id = purchase.Movie.Id,
                        Title = purchase.Movie.Title,
                     });
                }
            }

            var purchasedMovies = new PagedResultSet<MovieResponseModel>(data, page, pageSize, purchaseCount);
            return purchasedMovies;
        }

        public async Task<PaginatedList<MovieResponseModel>> GetAllPurchasesByMovieId(int movieId)
        {
            throw new System.NotImplementedException();
        }
    }
}
