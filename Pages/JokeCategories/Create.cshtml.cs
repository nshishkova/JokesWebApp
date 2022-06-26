using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using JokesWebApp.Models;

namespace JokesWebApp.Pages.JokeCategories
{
    public class CreateModel : PageModel
    {
        private readonly JokesWebApp.Models.JokesWebAppContext _context;

        public CreateModel(JokesWebApp.Models.JokesWebAppContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "CategoryId");
        ViewData["JokeId"] = new SelectList(_context.Set<Joke>(), "JokeId", "JokeId");
            return Page();
        }

        [BindProperty]
        public JokeCategory JokeCategory { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.JokeCategory.Add(JokeCategory);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
