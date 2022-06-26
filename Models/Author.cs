using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JokesWebApp.Models
{
    public class Author
    {
        public int AuthorId { get; set; }
        [Required(ErrorMessage = "The Name field is required.")]
        public String Name { get; set; }

        //public String FirstName { get; set; }
        // public String LastName { get; set; }

        [EmailAddress]
        public String Email { get; set; }



        public ICollection<Joke> Jokes { get; set; }
    }
}
