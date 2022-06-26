using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using JokesWebApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace JokesWebApp.Models
{
    public class JokesWebAppContext : IdentityDbContext
    {
        public JokesWebAppContext (DbContextOptions<JokesWebAppContext> options)
            : base(options)
        {
        }

        public DbSet<JokesWebApp.Models.Author> Author { get; set; }

        public DbSet<JokesWebApp.Models.Category> Category { get; set; }

        public DbSet<JokesWebApp.Models.JokeCategory> JokeCategory { get; set; }

        public DbSet<JokesWebApp.Models.Joke> Joke { get; set; }
    }
}
