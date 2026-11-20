using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movies.Models;

public class GenreController : Controller
{
    private readonly MoviesContext _context;

    public GenreController(MoviesContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var genres = await _context.Genre
            .Include(genre => genre.Movies)
            .OrderBy(genre => genre.Name)
            .ToListAsync();

        return View(genres);
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var genre = await _context.Genre
            .Include(genre => genre.Movies.OrderBy(movie => movie.Title))
            .FirstOrDefaultAsync(genre => genre.Id == id);

        if (genre == null)
        {
            return NotFound();
        }

        return View(genre);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Name")] Genre genre)
    {
        if (!ModelState.IsValid)
        {
            return View(genre);
        }

        _context.Genre.Add(genre);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
