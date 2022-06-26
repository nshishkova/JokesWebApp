using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JokesWebApp.Models;

namespace JokesWebApp.Pages.JokeCategories
{
    public class EditModel : PageModel
    {
        private readonly JokesWebApp.Models.JokesWebAppContext _context;

        public EditModel(JokesWebApp.Models.JokesWebAppContext context)
        {
            _context = context;
        }

        [BindProperty]
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
           ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "CategoryId");
           ViewData["JokeId"] = new SelectList(_context.Set<Joke>(), "JokeId", "JokeId");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(JokeCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JokeCategoryExists(JokeCategory.JokeCategoryId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool JokeCategoryExists(int id)
        {
            return _context.JokeCategory.Any(e => e.JokeCategoryId == id);
        }
    }
}
