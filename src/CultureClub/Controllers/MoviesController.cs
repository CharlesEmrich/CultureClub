using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CultureClub.Models;
using Microsoft.EntityFrameworkCore;

namespace CultureClub.Controllers
{
    public class MoviesController : Controller
    {
        private CultureClubDbContext db = new CultureClubDbContext();

        public IActionResult Index()
        {
            return View(db.Movies.ToList());
        }

        public IActionResult Details(int id)
        {
            var thisMovie = db.Movies.FirstOrDefault(movie => movie.MovieId == id);

            return View(thisMovie);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Movie movie)
        {
            db.Movies.Add(movie);
            db.SaveChanges();
            return RedirectToAction("Index");

        }
        public IActionResult Delete(int id)
        {
            var thisLoc = db.Movies.FirstOrDefault(movie => movie.MovieId == id);
            return View(thisLoc);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var thisLoc = db.Movies.FirstOrDefault(movie => movie.MovieId == id);
            db.Movies.Remove(thisLoc);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var thisLoc = db.Movies.FirstOrDefault(movie => movie.MovieId == id);
            return View(thisLoc);
        }

        [HttpPost]
        public IActionResult Edit(Movie movie)
        {
            db.Entry(movie).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Error()
        {
            return View();
        }
    }
}
