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
    public class IndexModel : PageModel
    {
        private readonly JokesWebApp.Models.JokesWebAppContext _context;

        public IndexModel(JokesWebApp.Models.JokesWebAppContext context)
        {
            _context = context;
        }

        public IList<JokeCategory> JokeCategory { get;set; }

        public async Task OnGetAsync()
        {
            JokeCategory = await _context.JokeCategory
                .Include(j => j.Category)
                .Include(j => j.Joke).ToListAsync();
        }
    }
}
