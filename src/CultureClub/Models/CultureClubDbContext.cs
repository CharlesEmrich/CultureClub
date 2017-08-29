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
        public RPGDbContext(DbContextOptions options) : base(options)
        {

        }
        public RPGDbContext()
        {

        }

        public DbSet<Movie> Movies { get; set; }

    }
}
