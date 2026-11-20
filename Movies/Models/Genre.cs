using System.ComponentModel.DataAnnotations;

namespace Movies.Models
{
    public class Genre
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public List<Movie> Movies { get; set; } = new List<Movie>();
    }
}
