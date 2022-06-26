using JokesWebApp.JokesWebViewModels;
using JokesWebApp.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JokesWebApp.Pages.Jokes
{
    public class JokeCategoryPageModel : PageModel
    {
        public List<JokeCategoryData> JokeCategoryDataList;
        public void PopulateJokeCategotyData(JokesWebAppContext context, Joke joke)
        {
            var allCategories = context.Category;

             var jokeCategpris = new HashSet<int>(joke.JokeCategories.Select(c => c.CategoryId));
             JokeCategoryDataList = new List<JokeCategoryData>();
             foreach (var group in allCategories)
             {
                JokeCategoryDataList.Add(new JokeCategoryData
                { 
                    CategoryId = group.CategoryId,
                    name = group.CategoryName
                }
                );
             }
        }

        public void UpdateJokeCategory(JokesWebAppContext context, string[] selectedCategories, Joke jokeToUpdate)
        {
            if (selectedCategories == null)
            {
                jokeToUpdate.JokeCategories = new List<JokeCategory>(); return;
            }

            var selectedCategoriesHS = new HashSet<string>(selectedCategories);
            var jokeCategories = new HashSet<int>(jokeToUpdate.JokeCategories.Select(c => c.Category.CategoryId)); 

            foreach (var gr in context.Category)
            {
                if (selectedCategoriesHS.Contains(gr.CategoryId.ToString()))
                {
                    if (!jokeCategories.Contains(gr.CategoryId))
                    {
                        jokeToUpdate.JokeCategories.Add(new JokeCategory
                        {
                           JokeId = jokeToUpdate.JokeId,
                           CategoryId = gr.CategoryId
                        });

                    }
                }
                else
                {
                    if (jokeCategories.Contains(gr.CategoryId))
                    {
                        JokeCategory categoryToRemove
                            = jokeToUpdate
                                .JokeCategories
                                .SingleOrDefault(i => i.CategoryId == gr.CategoryId);
                        context.Remove(categoryToRemove); ;
                    }
                }
            }

            //foreach (var jokeCategory in jokeToUpdate.JokeCategories)
            //{
            //    context.JokeCategory.Add(jokeCategory);
            //}
        }
    }
}
