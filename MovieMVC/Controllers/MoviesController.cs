using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieMVC.Controllers
{
    public class MoviesController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult TopRevenueMovies()
        {
            return View();
        }

        [HttpGet]
        public IActionResult TopRatedMovies()
        {
            return View();
        }

        // GET Movies By Genre id
        [HttpGet]
        public IActionResult Genre(int genreId)
        {
            return View();
        }

        // GET all Reviews of Movie id
        [HttpGet]
        public IActionResult Reviews(int movieId)
        {
            return View();
        }
    }
}
