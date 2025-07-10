using System.ComponentModel.DataAnnotations;

namespace Movies.Core.Validations
{
    public class ValidGenre : ValidationAttribute
    {
        private static readonly HashSet<string> AllowedGenres = new()
        {
            "Action", "Drama", "Comedy", "Horror", "Documentary", "Fantasy"
        };

        public override bool IsValid(object? value)
        {
            if (value is not string genre || string.IsNullOrWhiteSpace(genre))
                return false;

            return AllowedGenres.Contains(genre.Trim());
        }

    }
}
