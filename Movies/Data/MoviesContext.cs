using Microsoft.EntityFrameworkCore;

public class MoviesContext(DbContextOptions<MoviesContext> options) : DbContext(options)
{
    public DbSet<Movies.Models.Movie> Movie { get; set; } = default!;
    public DbSet<Movies.Models.Genre> Genre { get; set; } = default!;
}
