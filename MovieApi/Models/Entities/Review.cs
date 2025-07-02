﻿using System.ComponentModel.DataAnnotations;

namespace MovieApi.Models.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public string ReviewerName { get; set; } = null!;
        public string Comment { get; set; } = null!;
        [Range(1, 5)]
        public int Rating { get; set; }
        public Movie Movie { get; set; } = null!;
        public int MovieId { get; set; }
    }
}
