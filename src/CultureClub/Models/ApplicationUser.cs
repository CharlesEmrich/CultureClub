using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CultureClub.Models
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<Rating> Ratings { get; set; }
    }
}
