﻿using System.ComponentModel.DataAnnotations;

namespace Movies.Core.Models.Entities
{
    public class Actor : Entity
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        public int BirthYear { get; set; }

        public ICollection<MovieActor> MovieActors { get; set; } = new List<MovieActor>();
    }
}
