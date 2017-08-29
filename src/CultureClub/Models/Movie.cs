using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CultureClub.Models
{
    [Table("Movies")]
    public class Movie
    {
        [Key]
        public int MovieId { get; set; }
        public string Title { get; set; }
        public string Director { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
        //Do I want to track actors or genres here?
    }
}
