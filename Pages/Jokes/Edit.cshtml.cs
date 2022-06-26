using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JokesWebApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace JokesWebApp.Pages.Jokes
{
    [Authorize]
    public class EditModel : JokeCategoryPageModel
    {
        private readonly JokesWebApp.Models.JokesWebAppContext _context;

        public EditModel(JokesWebApp.Models.JokesWebAppContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Joke Joke { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }


            Joke = await _context.Joke
                .Include(e => e.JokeCategories).ThenInclude(en => en.Category)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.JokeId == id);


            if (Joke == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "Name");
            ViewData["JokeId"] = new SelectList(_context.Joke, "JokeId", "Name");
            PopulateJokeCategotyData(_context, Joke);

            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int? id, string[] selectedCategories)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Joke).State = EntityState.Modified;

            var JokeToUpdate = await _context.Joke
            .Include(e => e.JokeCategories)
            .ThenInclude(en => en.Category)
            .FirstOrDefaultAsync(m => m.JokeId == id);

            try
            {
                if (await TryUpdateModelAsync<Joke>
                    (JokeToUpdate, "Joke", i => i.JokeId, i=>i.JokeQuestion, i => i.JokeAnswer, i => i.JokeDate, i => i.JokeCategories)
                    )
                {
                    UpdateJokeCategory(_context, selectedCategories, JokeToUpdate);
                    await _context.SaveChangesAsync();
                    return RedirectToPage("./Index");
                }
           }

              catch (DbUpdateConcurrencyException)
            {
                if (!JokeExists(Joke.JokeId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            UpdateJokeCategory(_context, selectedCategories, JokeToUpdate);
            PopulateJokeCategotyData(_context, JokeToUpdate);
            return Page();
           
        }

        private bool JokeExists(int id)
        {
            return _context.Joke.Any(e => e.JokeId== id);
        }
    }
}
