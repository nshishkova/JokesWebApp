using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using JokesWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace JokesWebApp.Pages.Jokes
{
    [Authorize]
    public class CreateModel : JokeCategoryPageModel
    {
        private readonly JokesWebApp.Models.JokesWebAppContext _context;

        public CreateModel(JokesWebApp.Models.JokesWebAppContext context)
        {
            _context = context;
        }

     

        public IActionResult OnGet()
        {
            var joke = new Joke();
            joke.JokeCategories = new List<JokeCategory>();

            PopulateJokeCategotyData(_context, joke);
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "Name");

            ViewData["JokeId"] = new SelectList(_context.Joke, "JokeID", "Name");

            return Page();

            


        }



        [BindProperty]
        public Joke Joke { get; set; }

       
        public async Task<IActionResult> OnPostAsync(string[] selectedCategories)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var newJoke = new Joke();
            if (selectedCategories != null)
            {
                newJoke.JokeCategories = new List<JokeCategory>();
                foreach (var gr in selectedCategories)
                {
                    var categoryToAdd = new JokeCategory
                    {
                        CategoryId = int.Parse(gr)
                    };
                    newJoke.JokeCategories.Add(categoryToAdd);
                }
            }
            if (await TryUpdateModelAsync<Joke>(newJoke, "Joke",
                i => i.JokeId, i => i.JokeQuestion, i => i.JokeAnswer, i => i.JokeDate, i => i.JokeCategories))
            {
                var entity = new Author() { Name = Joke.Author.Name };
                _context.Author.Add(entity);
                await _context.SaveChangesAsync();

                newJoke.Author = entity;
                _context.Joke.Add(newJoke);
                await _context.SaveChangesAsync();
                //    HttpContext.Session.SetString("SuccessMsg", "The joke was successully added.");

                //}
                //else
                //{
                //    HttpContext.Session.SetString("ErorrMsg", "Greshka pri dobavqne na shega.");
                //}
                return RedirectToPage("./Index");
            }
                PopulateJokeCategotyData(_context, newJoke);
                return Page();
            }
        }
    }
