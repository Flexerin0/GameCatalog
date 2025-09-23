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
        public IndexModel(AppDbContext context) => _context = context;

        public IList<Game> Games { get; set; } = new List<Game>();

        public async Task OnGetAsync()
        {
            Games = await _context.Games
                .Include(g => g.Genre)
                .ToListAsync();
        }
    }
}
