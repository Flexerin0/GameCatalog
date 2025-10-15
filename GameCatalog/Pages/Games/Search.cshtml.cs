using GameCatalog.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GameCatalog.Pages.Games
{
    public class SearchModel : PageModel
    {
        private readonly AppDbContext _context;
        public SearchModel(AppDbContext context) => _context = context;

        public async Task<IActionResult> OnGetAsync(string term)
        {
            var query = _context.Games.Include(g => g.Genre).AsQueryable();

            if (!string.IsNullOrWhiteSpace(term))
            {
                var lowerTerm = term.ToLower();
                query = query.Where(g =>
                    g.Title.ToLower().Contains(lowerTerm) ||
                    g.Description.ToLower().Contains(lowerTerm));
            }

            var results = await query
                .Select(g => new
                {
                    g.Id,
                    g.Title,
                    g.Description,
                    g.ImageUrl,
                    Genre = g.Genre != null ? g.Genre.Name : "Не указан",
                    g.Developer,
                    g.ReleaseYear
                })
                .ToListAsync();

            return new JsonResult(results);
        }
    }
}
