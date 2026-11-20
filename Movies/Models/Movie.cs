using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Movies.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateOnly ReleaseDate { get; set; }
        public TimeOnly Duration { get; set; }
        [NotMapped]
        [Display(Name = "Hours")]
        [Range(0, 23)]
        public int DurationHours { get; set; }
        [NotMapped]
        [Display(Name = "Minutes")]
        [Range(0, 59)]
        public int DurationMinutes { get; set; }
        public int GenreId { get; set; }
        [ValidateNever]
        public Genre Genre { get; set; } = null!;
    }
}
