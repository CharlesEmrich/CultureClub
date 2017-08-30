﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CultureClub.Models
{
    [Table("Ratings")]
    public class Rating
    {
        [Key]
        public int RatingId { get; set; }
        public virtual Movie Movie { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        //Also we probably need some way to relate this Rating to the user who issued it?
        //Aw heck. How actually does this get its relationship to movies? It needs a foreign key, right? Consult FixIt
        public int Score { get; set; }
    }

    public enum ScoreNumber
    {
        One=1, Two, Three, Four, Five
    }
}
