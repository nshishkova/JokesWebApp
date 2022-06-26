using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JokesWebApp.Models
{
    public class Joke
    {
        public int JokeId { get; set; }
        [Display(Name = "Joke Question")]
        [Required(ErrorMessage = "The Joke Question is required.")]
        public String JokeQuestion { get; set; }
        [Display(Name = "Joke Answer")]
        [Required(ErrorMessage = "The Joke Answer is required.")]

        public String JokeAnswer { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Date")]

        public DateTime JokeDate { get; set; }

        public Author Author { get; set; }

        [Display(Name = "Categories")]
        public ICollection<JokeCategory> JokeCategories { get; set; }
    }
}
