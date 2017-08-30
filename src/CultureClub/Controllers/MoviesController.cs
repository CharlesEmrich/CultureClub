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

            Rating newRating = new Rating { Movie = thisMovie, Score = (int)Rating, ApplicationUser = currentUser };

            _db.Ratings.Add(newRating);
            _db.SaveChanges();

            return View(thisMovie);
        }
    }
}
