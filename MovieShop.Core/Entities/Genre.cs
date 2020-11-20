using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MovieShop.Core.Entities
{
    
    [Table("Genre")]
    public class Genre
    {
        public int Id { get; set; }
        [MaxLength(24)]
        public string Name { get; set; }

        public ICollection<Movie> Movies { get; set; }
        public ICollection<MovieGenre> MovieGenres { get; set; }
    }
}
