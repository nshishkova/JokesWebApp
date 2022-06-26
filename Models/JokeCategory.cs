using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace JokesWebApp.Models
{
    public class JokeCategory
    {
        public int JokeCategoryId { get; set; }

        public int JokeId { get; set; }
        public Joke Joke { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

      
        }
    }

