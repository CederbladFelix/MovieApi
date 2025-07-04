using System.ComponentModel.DataAnnotations;

namespace MovieApi.Validations
{
    public class ValidGenre : ValidationAttribute
    {
        private readonly List<string> genreNames = new()
        {
            "Action", "Drama", "Comedy", "Horror", "Documentary", "Fantasy"
        };

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is string input)
            {
                if (genreNames.Contains(input))
                    return ValidationResult.Success;
            }

            return new ValidationResult($"Invalid genre. Allowed genres: {string.Join(", ", genreNames)}");
        }
    }
}
