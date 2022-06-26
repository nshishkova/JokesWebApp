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
    public class PaginatedList<T> : List<T>
    {
        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }
        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            this.AddRange(items);
        }
        public bool HasPreviousPage { get { return (PageIndex > 1); } }
        public bool HasNextPage { get { return (PageIndex < TotalPages); } }
        public static async Task<PaginatedList<T>> CreateAsync(
        IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip(
            (pageIndex - 1) * pageSize)
            .Take(pageSize).ToListAsync();
            return new PaginatedList<T>(items, count, pageIndex, pageSize);
           }
        }
        public class IndexModel : PageModel
         {
        private readonly JokesWebApp.Models.JokesWebAppContext _context;

        public IndexModel(JokesWebApp.Models.JokesWebAppContext context)
        {
            _context = context;
        }

        public PaginatedList<Joke> Joke { get; set; }
        public async Task OnGetAsync(string sortOrder, string searchString, string currentFilter, int? pageIndex)
        {
            currentFilter = searchString;

            JokeQuestionSort = String.IsNullOrEmpty(sortOrder) ? "JokeQuestion_desc" : "";

            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            currentSort = sortOrder;
            currentFilter = searchString;
            IQueryable<Joke> jokes = from s in _context.Joke
                                     select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                jokes = jokes.Where(s => s.JokeQuestion.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "JokeQuestion_desc":
                    jokes = jokes.OrderByDescending(s => s.JokeQuestion);
                    break;
                default: 
                    jokes = jokes.OrderBy(s => s.JokeQuestion);
                    break;
            }
            int pageSize = 5;
            Joke = await PaginatedList<Joke>.CreateAsync(jokes.AsNoTracking(), pageIndex ?? 1, pageSize);
        }
        public string currentFilter { get; set; }
        public string currentSort { get; set; }
        public string JokeQuestionSort { get; set; }
        }
}
