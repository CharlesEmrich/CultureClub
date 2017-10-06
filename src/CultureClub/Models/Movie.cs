using System.Collections.Generic;
using System.Linq;
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

            if (movieRatings.Count > 0)
            {
                int total = 0;
                movieRatings.ForEach(e => total += e.Score);
                double average = ((double)total / (double)movieRatings.Count);
                Debug.WriteLine(total);
                Debug.WriteLine(movieRatings.Count);
                Debug.WriteLine(total / movieRatings.Count);
                //TODO: Get this to display with some decimals?
                return average;
            } else
            {
                return 0;
            }
        }

        public int CurrentUserRating (string UserId, int MovieId)
        {
            Rating MyRating = new CultureClubDbContext().Ratings.FirstOrDefault(e => e.Movie.MovieId == MovieId && e.ApplicationUser.Id == UserId);
            return MyRating.Score;
        }
    }
}
