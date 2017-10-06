using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using CultureClub.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;
using Newtonsoft.Json;

namespace CultureClub.Controllers
{
    public class MoviesController : Controller
    {
        private CultureClubDbContext _db = new CultureClubDbContext();
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public MoviesController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, CultureClubDbContext db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _db = db;
        }

        public IActionResult Index()
        {
            return View(_db.Movies.ToList());
        }

        public IActionResult Details(int id)
        {
            var thisMovie = _db.Movies.FirstOrDefault(movie => movie.MovieId == id);

            return View(thisMovie);
        }

        public IActionResult Create()
        {
            return View();
        }


        public IActionResult Faves()
        {
            ViewData["Message"] = "Your Favorite Movies.";
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Debug.WriteLine(userId);

            //Movies the current user rated highly.
            var userFaves = _db.Ratings.Include(e => e.Movie).Where(e => e.ApplicationUser.Id == userId && e.Score > 3).ToList();

            List<Movie> faveMovies = new List<Movie> { };
            userFaves.ForEach(e => faveMovies.Add(e.Movie));

            return View(faveMovies);
        }

        public IActionResult FaveRelated(int id)
        {
            //Find the movie we're talking about.
            var thisMovie = _db.Movies.FirstOrDefault(movie => movie.MovieId == id);
            //Find all high ratings for that movie.
            var highRatings = _db.Ratings.Include(e => e.ApplicationUser).Include(e => e.Movie).Where(e => e.Movie.MovieId == thisMovie.MovieId && e.Score > 3).ToList();
                Debug.WriteLine("There are " + highRatings.Count + " high ratings for " + thisMovie.Title);
            foreach (Rating rating in highRatings)
            {
                Debug.WriteLine(rating.ApplicationUser.UserName + " rated it highly.");
            }
            //Find all of the high ratings issued by users who rated this movie highly.
            List<Movie> relatedMovies = new List<Movie> { };
            highRatings.ForEach(rating =>
            {
                if (rating.ApplicationUser.Id != User.FindFirst(ClaimTypes.NameIdentifier)?.Value)
                {
                    foreach (Rating foreignRating in rating.ApplicationUser.Ratings)
                    {
                        Debug.WriteLine("User " + rating.ApplicationUser.UserName + " rated " + foreignRating.Movie?.Title + " a " + foreignRating.Score);
                        Debug.WriteLine(foreignRating.Movie.Title + "!=" + thisMovie.Title);
                        if (foreignRating.Movie.Title != thisMovie.Title && foreignRating.Score > 3 && !relatedMovies.Contains(foreignRating.Movie))
                        {
                            relatedMovies.Add(foreignRating.Movie);
                        }
                    }

                    //TODO: It seems like it might be possible to refactor the above into something more similar to the below:
                    //rating.ApplicationUser
                    //.Ratings.Where(userRating => userRating.Score > 3).ToList()
                    //.ForEach(userRating => relatedMovies.Add(rating.Movie));
                }
            });
            return View(relatedMovies);
        }

        [HttpPost]
        public IActionResult Create(Movie movie)
        {
            _db.Movies.Add(movie);
            _db.SaveChanges();
            return RedirectToAction("Index");

        }
        public IActionResult Delete(int id)
        {
            var thisMovie = _db.Movies.FirstOrDefault(movie => movie.MovieId == id);
            return View(thisMovie);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var thisMovie = _db.Movies.FirstOrDefault(movie => movie.MovieId == id);
            _db.Movies.Remove(thisMovie);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var thisMovie = _db.Movies.FirstOrDefault(movie => movie.MovieId == id);
            return View(thisMovie);
        }

        [HttpPost]
        public IActionResult Edit(Movie movie)
        {
            _db.Entry(movie).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Error()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Details(int MovieId, ScoreNumber Rating)
        {
            var thisMovie = _db.Movies.FirstOrDefault(movie => movie.MovieId == MovieId);
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);

            if (Rating > 0)
            {

                Rating newRating = new Rating { Movie = thisMovie, Score = (int)Rating, ApplicationUser = currentUser };

                _db.Ratings.Add(newRating);
                _db.SaveChanges();
            }
            return View(thisMovie);
        }
    }
}
