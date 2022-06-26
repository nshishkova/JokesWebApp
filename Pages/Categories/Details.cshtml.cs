using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using JokesWebApp.Models;

namespace JokesWebApp.Pages.Categories
{
    public class DetailsModel : PageModel
    {
        private readonly JokesWebApp.Models.JokesWebAppContext _context;

        public DetailsModel(JokesWebApp.Models.JokesWebAppContext context)
        {
            _context = context;
        }

        public Category Category { get; set; }

        public List<JokeCategory> Jokes { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Category = await _context.Category
                .Include(j => j.JokeCategories)
                    .ThenInclude(j => j.Joke)
                .FirstOrDefaultAsync(m => m.CategoryId == id);

            Jokes = Category.JokeCategories.ToList();

            if (Category == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
