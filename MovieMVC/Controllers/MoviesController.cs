using Microsoft.AspNetCore.Mvc;
using MovieShop.Core.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieMVC.Controllers
{
    public class MoviesController : Controller
    {

        private readonly IMovieService _movieService;
        public MoviesController(IMovieService moiveService)
        {
            _movieService = moiveService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            // call the movie service
            var movieDetails = _movieService.GetMovieById(id);
            return View(movieDetails);
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
