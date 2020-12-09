using System;
using System.Collections.Generic;
using System.Text;

namespace MovieShop.Core.Models.Response_Model
{
    public class ReviewResponseModel
    {
        public int UserId { get; set; }
        public List<ReviewMovieResponseModel> MovieReviews { get; set; }
    }
}
