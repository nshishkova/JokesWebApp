using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using JokesWebApp.Models;

namespace JokesWebApp.Pages.Jokes
{
    public class DetailsModel : PageModel
    {
        private readonly JokesWebApp.Models.JokesWebAppContext _context;

        public DetailsModel(JokesWebApp.Models.JokesWebAppContext context)
        {
            _context = context;
        }

        public Joke Joke { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //Joke = await _context.Joke
            //    .Include(p => p.Author)
            //    .FirstOrDefaultAsync(m => m.JokeId == id);

            Joke = await _context.Joke
             .Include(x => x.JokeCategories)
             .ThenInclude(x => x.Category)
             .Include(x => x.Author)
             .FirstOrDefaultAsync(m => m.JokeId == id);


            if (Joke == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
