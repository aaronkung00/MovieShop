using System;
using System.Collections.Generic;
using System.Text;

namespace MovieShop.Core.Models.Request_Model
{
    public class FavoriteRequestModel
    {
        public int UserId { get; set; }
        public int MovieId { get; set; }
    }
}
