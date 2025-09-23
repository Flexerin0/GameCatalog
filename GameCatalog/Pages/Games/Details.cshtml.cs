using GameCatalog.Data;
using GameCatalog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GameCatalog.Pages.Games
{
    public class DetailsModel : PageModel
    {
        private readonly AppDbContext _context;
        public DetailsModel(AppDbContext context) => _context = context;

        public Game? Game { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Game = await _context.Games
                .Include(g => g.Genre)
                .FirstOrDefaultAsync(g => g.Id == id);

            if (Game == null) return NotFound();
            return Page();
        }
    }
}
