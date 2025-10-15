using GameCatalog.Data;
using GameCatalog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GameCatalog.Pages.Games
{
    public class EditModel : PageModel
    {
        private readonly AppDbContext _context;

        public EditModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Game Game { get; set; } = default!;

        public SelectList Genres { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Game = await _context.Games
                .Include(g => g.Genre)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (Game == null)
            {
                return NotFound();
            }

            Genres = new SelectList(_context.Genres, "Id", "Name", Game.GenreId);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Genres = new SelectList(_context.Genres, "Id", "Name", Game.GenreId);
                return Page();
            }

            var gameToUpdate = await _context.Games.FindAsync(Game.Id);
            if (gameToUpdate == null) return NotFound();

            if (await TryUpdateModelAsync<Game>(gameToUpdate, "Game",
                g => g.Title, g => g.Developer, g => g.ReleaseYear, g => g.GenreId, g => g.Description, g => g.ImageUrl))
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("Index");
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Games.Any(e => e.Id == Game.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("Index");
        }
    }
}
