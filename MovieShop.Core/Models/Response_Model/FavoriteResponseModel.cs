using System;
using System.Collections.Generic;
using System.Text;

namespace MovieShop.Core.Models.Response_Model
{
    public class FavoriteResponseModel
    {
        public List<FavoriteMovieResponseModel> FavoriteMovies { get; set; }
        public class FavoriteMovieResponseModel : MovieResponseModel
        {
        }
    }
}
