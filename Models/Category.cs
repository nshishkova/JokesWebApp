using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JokesWebApp.Models
{
    public class Category
    {

        public int CategoryId { get; set; }
        [Display(Name = "Category Name")]
        [Required(ErrorMessage = "The Category Name field is required.")]
        public String CategoryName { get; set; }

        public ICollection<JokeCategory> JokeCategories { get; set; }
    }
}
