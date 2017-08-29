using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace CultureClub.Models
{
    public class CultureClubDbContext : IdentityDbContext<ApplicationUser>
    {
        public CultureClubDbContext(DbContextOptions options) : base(options)
        {

        }
        public CultureClubDbContext()
        {

        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Rating> Ratings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=RPG;integrated security=True");
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
