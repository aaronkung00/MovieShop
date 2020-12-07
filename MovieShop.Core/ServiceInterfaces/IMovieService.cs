using System.Collections.Generic;
using System.Threading.Tasks;
using MovieShop.Core.Entities;
using MovieShop.Core.Models;
using MovieShop.Core.Models.Request_Model;

namespace MovieShop.Core.ServiceInterfaces
{
    public interface IMovieService
    {
        /*
        Task<PagedResultSet<MovieResponseModel>> GetMoviesByPagination(int pageSize = 20, int page = 0, string title = "");
        Task<PagedResultSet<MovieResponseModel>> GetAllMoviePurchasesByPagination(int pageSize = 20, int page = 0);
        Task<PaginatedList<MovieResponseModel>> GetAllPurchasesByMovieId(int movieId);
        Task<MovieDetailsResponseModel> GetMovieAsync(int id);
        Task<IEnumerable<ReviewMovieResponseModel>> GetReviewsForMovie(int id);
        Task<int> GetMoviesCount(string title = "");
        Task<IEnumerable<MovieResponseModel>> GetTopRatedMovies();
        Task<IEnumerable<MovieResponseModel>> GetHighestGrossingMovies();
        Task<IEnumerable<MovieResponseModel>> GetMoviesByGenre(int genreId);
        Task<MovieDetailsResponseModel> CreateMovie(MovieCreateRequest movieCreateRequest);
        Task<MovieDetailsResponseModel> UpdateMovie(MovieCreateRequest movieCreateRequest);
        */

        // inclass
        Task<IEnumerable<MovieResponseModel>> GetHighestRevenueMovies();

        //HW
        Task<MovieDetailsResponseModel> GetMovieAsync(int id);
        Task<IEnumerable<MovieResponseModel>> GetTopRatedMovies();
        Task<IEnumerable<MovieResponseModel>> GetMoviesByGenre(int genreId);
        Task<IEnumerable<Review>> GetMovieReviews(int id);
        Task<MovieDetailsResponseModel> CreateMovie(MovieCreateRequest movieCreateRequest);
    }
}
