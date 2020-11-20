﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MovieShop.Core.Entities
{
    public class Crew
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string TmdbUrl { get; set; }
        public string ProfilePath { get; set; }
        // Navigator
        public ICollection<MovieCrew> MovieCrews { get; set; }
    }
}
