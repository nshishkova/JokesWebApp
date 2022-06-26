using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using JokesWebApp.Models;

namespace JokesWebApp.Pages.JokeCategories
{
    public class DetailsModel : PageModel
    {
        private readonly JokesWebApp.Models.JokesWebAppContext _context;

        public DetailsModel(JokesWebApp.Models.JokesWebAppContext context)
        {
            _context = context;
        }

        public JokeCategory JokeCategory { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            JokeCategory = await _context.JokeCategory
                .Include(j => j.Category)
                .Include(j => j.Joke).FirstOrDefaultAsync(m => m.JokeCategoryId == id);

            if (JokeCategory == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
