using GameCatalog.Data;
using GameCatalog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GameCatalog.Pages.Games
{
    public class CreateModel : PageModel
    {
        private readonly AppDbContext _context;
        public CreateModel(AppDbContext context) => _context = context;

        [BindProperty]
        public Game Game { get; set; }

        public List<SelectListItem> Genres { get; set; } = new();

        public async Task OnGetAsync()
        {
            Genres = await _context.Genres
                .Select(g => new SelectListItem { Value = g.Id.ToString(), Text = g.Name })
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await OnGetAsync();
                return Page();
            }

            _context.Games.Add(Game);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}
