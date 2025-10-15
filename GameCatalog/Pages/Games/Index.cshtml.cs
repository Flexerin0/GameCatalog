using GameCatalog.Data;
using GameCatalog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GameCatalog.Pages.Games
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;
        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }
        public IndexModel(AppDbContext context) => _context = context;

        public IList<Game> Games { get; set; } = new List<Game>();
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; }

        private const int PageSize = 6;

        public async Task OnGetAsync(int pageNumber = 1)
        {
            CurrentPage = pageNumber;

            var totalGames = await _context.Games.CountAsync();
            TotalPages = (int)Math.Ceiling(totalGames / (double)PageSize);

            Games = await _context.Games
                .Include(g => g.Genre)
                .OrderBy(g => g.Title)
                .Skip((pageNumber - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();
        }
    }
}
