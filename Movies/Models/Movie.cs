using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Movies.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateOnly ReleaseDate { get; set; }
        public TimeOnly Duration { get; set; }
        public int GenreId { get; set; }
        [ValidateNever]
        public Genre Genre { get; set; } = null!;
    }
}
