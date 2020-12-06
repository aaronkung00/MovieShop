using MovieShop.Core.Models;
using MovieShop.Core.RepositoryInterfaces;
using MovieShop.Core.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieShop.Infrastructure.Services
{
    public class CastService : ICastService
    {
        private readonly ICastRepository _castRepository;
        public CastService(ICastRepository castRepository)
        {
            _castRepository = castRepository;
        }
        public async Task<CastDetailsResponseModel> GetCastDetailsWithMovies(int id)
        {
            var cast = await _castRepository.GetByIdAsync(id);
            var respose = new CastDetailsResponseModel {

                Id = cast.Id, Name = cast.Name, Gender = cast.Gender,
                ProfilePath = cast.ProfilePath, TmdbUrl = cast.TmdbUrl               
            };

            var castMovies  = new List<MovieResponseModel>();


            foreach (var m in cast.MovieCasts)
            {
              
                    var movieResponse = new MovieResponseModel { 
                        Id = m.Movie.Id, Title = m.Movie.Title ,PosterUrl = m.Movie.PosterUrl, 
                        ReleaseDate = m.Movie.ReleaseDate.Value
                    };

                    castMovies.Add(movieResponse);

            }

            respose.Movies = castMovies;

            return respose;
        }
    }
}
