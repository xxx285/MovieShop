using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieShop.Core.Models.Request;
using MovieShop.Core.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MovieMVC.Controllers
{
    public class UserController : Controller
    {
        private readonly IMovieService _movieService;
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICurrentLogedInUser _currentLogedInUser;
        public UserController(IMovieService movieService, IUserService userService, IHttpContextAccessor httpContextAccessor, ICurrentLogedInUser currentLogedInUser)
        {
            _movieService = movieService;
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
            _currentLogedInUser = currentLogedInUser;
        }
        
        [HttpGet]
        public async Task<IActionResult> Buy(int id)
        {
            if (_currentLogedInUser.IsAuthenticated)
            {
                var movieBuy = await _movieService.GetMovieSummaryById(id);
                return View(movieBuy);
            }
            else
            {
                return LocalRedirect(Url.Content("~/account/login"));
            }
        }

        [HttpGet]
        // call UserService to save this movie, which is calling UserRepository that will save it to the purchase table
        public async Task<IActionResult> Purchase(int id)
        {
            try
            {
                int userId = Convert.ToInt32(_httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
                bool result = await _userService.PurchaseMovie(userId, id);
                if (result)
                    return View("PurchaseSuccess");
                return View("PurchaseFail");
            }
            catch
            {
                return View("PurchaseFail");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Review(int id)
        {
            if (_currentLogedInUser.IsAuthenticated)
            {
                var movieSummary = await _movieService.GetMovieSummaryById(id);
                ViewBag.PosterUrl = movieSummary.PosterUrl;
                ViewBag.Title = movieSummary.Title;
                ViewBag.Id = movieSummary.Id;
                return View();
            }
            else
            {
                return LocalRedirect(Url.Content("~/account/login"));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Review(ReviewRequestModel reviewRequestModel, int movieId)
        {
            try
            {
                int userId = Convert.ToInt32(_httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
                bool result = await _userService.ReviewMovie(reviewRequestModel, userId, movieId);
                if (result)
                    return View("ReviewSuccess");
                return View("ReviewFail");
            }
            catch
            {
                return View("ReviewFail");
            }
        }
    }
}
