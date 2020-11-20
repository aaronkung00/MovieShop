using System;
using System.Collections.Generic;
using System.Text;

namespace MovieShop.Core.Entities
{
    public class Trailer
    {
        public int Id { get; set; }
        // FK from Movie table which is Id as PK there
        public int MovieId { get; set; }
        public string TrailerUrl { get; set; }
        public string Name { get; set; }

        // Navigation Properties, help us navigate to related enetities
        // trailerID 24 => get me Movie title and Movie overview
        // For UI side
        public Movie Movie { get; set; }
    }
}
