using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CultureClub.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CultureClub.Controllers
{
    public class RatingsController : Controller
    {
        private CultureClubDbContext db = new CultureClubDbContext();

        public IActionResult Create(int id)
        {
            var thisMovie = db.Movies.FirstOrDefault(movie => movie.MovieId == id);

            return View(thisMovie);
        }
        [HttpPost]
        public IActionResult Create(int MovieId, ScoreNumber Rating)
        {
            Debug.WriteLine("***********************\n\n***********************");
            Debug.WriteLine(MovieId);
            Debug.WriteLine(Rating);
            Debug.WriteLine("***********************\n\n***********************");

            return View();
        }
    }
}
