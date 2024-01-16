using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LearnMVC.Models;

namespace LearnMVC.Data
{
    public class LearnMVCContext : DbContext
    {
        public LearnMVCContext (DbContextOptions<LearnMVCContext> options)
            : base(options)
        {
        }

        public DbSet<LearnMVC.Models.Movie> Movie { get; set; } = default!;
        public DbSet<LearnMVC.Models.Book> Book { get; set; } = default!;
        public DbSet<LearnMVC.Models.Author> Author { get; set; } = default!;
    }
}
