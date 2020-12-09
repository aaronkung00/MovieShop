using System;
using System.Collections.Generic;
using System.Text;

namespace MovieShop.Core.Models.Request_Model
{
    public class ReviewRequestModel
    {
        public int UserId { get; set; }
        public int MovieId { get; set; }
        public string reviewText { get; set; }
        public decimal Rating { get; set; }
    }
}
