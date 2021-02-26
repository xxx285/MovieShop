using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieShop.Core.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }
        
        [HttpGet]
        [Route("toprevenue")]
        public async Task<IActionResult> GetTopRevenueMovies()
        {
            var movies = await _movieService.GetTop25GrossingMovies();

            if(!movies.Any())
            {
                return NotFound("We did not find any movie");
            }
            return Ok(movies);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetMovieById(int id) // parameter name should be matched
        {
            var movie = await _movieService.GetMovieById(id);
            if (movie == null)
                return NotFound();
            return Ok(movie);
        }

        [HttpGet]
        [Route("genre/{genreId:int}")]
        public async Task<IActionResult> GetMoviesByGenre(int genreId)
        {
            var movies = await _movieService.GetMoviesByGenre(genreId);
            if (movies == null)
                return NotFound();
            return Ok(movies);
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetMoviesByPage()
        {
            var movies = await _movieService.GetMoviesByPage();
            if (movies == null)
                return NotFound();
            return Ok(movies);
        }
        
        [HttpGet]
        [Route("{id:int}/reviews")]
        public async Task<IActionResult> GetMovieReviews(int id) // the params are matched by name so the name in [Route("")] must be the same with the function's params
        {
            var reviews = await _movieService.GetReviewsForMovie(id);
            if (reviews == null)
                return NotFound();
            return Ok(reviews);
        }

        [HttpGet]
        [Route("toprated")]
        public async Task<IActionResult> GetTopRatedMovies()
        {
            var movies = await _movieService.GetTop25RatedMovies();
            if (movies == null)
                return NotFound();
            return Ok(movies);
        }
    }
}
