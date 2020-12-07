using MovieShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieShop.Core.RepositoryInterfaces
{

     public interface IMovieRepository : IAsyncRepository<Movie>
        {
            Task<IEnumerable<Movie>> GetTopRatedMovies();
            Task<IEnumerable<Movie>> GetMoviesByGenre(int genreId);
            Task<IEnumerable<Movie>> GetHighestRevenueMovies();
            Task<IEnumerable<Review>> GetMovieReviews(int id);

            
    }
    
}
