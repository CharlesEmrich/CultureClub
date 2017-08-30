using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace CultureClub.Models
{
    [Table("Movies")]
    public class Movie
    {
        [Key]
        public int MovieId { get; set; }
        public string Title { get; set; }
        public string Director { get; set; }
        public int Year { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
        //Do I want to track actors or genres here?

        public List<Rating> GetRatings()
        {
            return new CultureClubDbContext().Ratings.Where(e => e.Movie.MovieId == MovieId).ToList();
        }

        public double AverageRating(int MovieId)
        {
            List<Rating> movieRatings = new CultureClubDbContext().Ratings.Where(e => e.Movie.MovieId == MovieId).ToList();

            int total = 0;
            movieRatings.ForEach(e => total += e.Score);
            double average = (double)(total / movieRatings.Count);
            Debug.WriteLine(total);
            Debug.WriteLine(movieRatings.Count);
            Debug.WriteLine(total / movieRatings.Count);
            //TODO: Get this to display with some decimals?
            return average;
        }
    }

}
