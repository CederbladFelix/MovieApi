﻿using System.ComponentModel.DataAnnotations;

namespace Movies.Core.Models.Entities
{
    public class Review : Entity
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string ReviewerName { get; set; } = null!;

        [MaxLength(500)]
        public string Comment { get; set; } = null!;

        [Range(1, 5)]
        public int Rating { get; set; }

        public Movie Movie { get; set; } = null!;
        public int MovieId { get; set; }
    }
}
