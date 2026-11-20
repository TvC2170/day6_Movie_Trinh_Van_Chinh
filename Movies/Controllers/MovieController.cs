
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Movies.Models;

public class MovieController : Controller
{
    private readonly MoviesContext _context;

    public MovieController(MoviesContext context)
    {
        _context = context;
    }

    // GET: MOVIES
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Movie.Include(movie => movie.Genre).ToListAsync());
    }

    // GET: MOVIES/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var movie = await _context.Movie
            .Include(movie => movie.Genre)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (movie == null)
        {
            return NotFound();
        }

        return View(movie);
    }

    // GET: MOVIES/Create
    public async Task<IActionResult> Create(int? genreId)
    {
        await PopulateGenreSelectList(genreId);
        return View(new Movie { GenreId = genreId ?? 0 });
    }

    // POST: MOVIES/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Title,ReleaseDate,DurationHours,DurationMinutes,GenreId")] Movie movie)
    {
        ValidateDuration(movie);
        if (ModelState.IsValid)
        {
            movie.Duration = new TimeOnly(movie.DurationHours, movie.DurationMinutes);
            _context.Add(movie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        await PopulateGenreSelectList(movie.GenreId);
        return View(movie);
    }

    // GET: MOVIES/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var movie = await _context.Movie.FindAsync(id);
        if (movie == null)
        {
            return NotFound();
        }
        movie.DurationHours = movie.Duration.Hour;
        movie.DurationMinutes = movie.Duration.Minute;
        await PopulateGenreSelectList(movie.GenreId);
        return View(movie);
    }

    // POST: MOVIES/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Title,ReleaseDate,DurationHours,DurationMinutes,GenreId")] Movie movie)
    {
        if (id != movie.Id)
        {
            return NotFound();
        }

        ValidateDuration(movie);
        if (ModelState.IsValid)
        {
            movie.Duration = new TimeOnly(movie.DurationHours, movie.DurationMinutes);
            try
            {
                _context.Update(movie);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(movie.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        await PopulateGenreSelectList(movie.GenreId);
        return View(movie);
    }

    // GET: MOVIES/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var movie = await _context.Movie
            .FirstOrDefaultAsync(m => m.Id == id);
        if (movie == null)
        {
            return NotFound();
        }

        return View(movie);
    }

    // POST: MOVIES/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var movie = await _context.Movie.FindAsync(id);
        if (movie != null)
        {
            _context.Movie.Remove(movie);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool MovieExists(int? id)
    {
        return _context.Movie.Any(e => e.Id == id);
    }

    private async Task PopulateGenreSelectList(int? selectedGenreId = null)
    {
        ViewData["GenreId"] = new SelectList(
            await _context.Genre.OrderBy(genre => genre.Name).ToListAsync(),
            "Id",
            "Name",
            selectedGenreId);
    }

    private void ValidateDuration(Movie movie)
    {
        if (movie.DurationHours == 0 && movie.DurationMinutes == 0)
        {
            ModelState.AddModelError(nameof(movie.DurationMinutes), "Duration must be greater than zero.");
        }
    }
}
