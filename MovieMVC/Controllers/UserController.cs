using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        public async Task<IActionResult> Buy(int id)
        {
            var movieBuy = await _movieService.GetMovieSummaryById(id);
            return View(movieBuy);

            // the filter [Authorize] would transform the code below int to the 2-line code above
            //if (_currentLogedInUser.IsAuthenticated)
            //{
            //    var movieBuy = await _movieService.GetMovieSummaryById(id);
            //    return View(movieBuy);
            //}
            //else
            //{
            //    return LocalRedirect(Url.Content("~/account/login"));
            //}
        }

        [HttpPost]
        [Authorize]
        // call UserService to save this movie, which is calling UserRepository that will save it to the purchase table
        public async Task<IActionResult> Buy(PurchaseRequestModel purchaseRequestModel)
        {
            try
            {
                int userId = _currentLogedInUser.UserId;
                bool result = await _userService.PurchaseMovie(purchaseRequestModel);
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
        [Authorize]
        public async Task<IActionResult> Review(int id)
        {
            var movieSummary = await _movieService.GetMovieSummaryById(id);
            ViewBag.PosterUrl = movieSummary.PosterUrl;
            ViewBag.Title = movieSummary.Title;
            ViewBag.Id = movieSummary.Id;
            return View();

            // the filter [Authorize] would transform the code below int to the 2-line code above
            //if (_currentLogedInUser.IsAuthenticated)
            //{
            //    var movieSummary = await _movieService.GetMovieSummaryById(id);
            //    ViewBag.PosterUrl = movieSummary.PosterUrl;
            //    ViewBag.Title = movieSummary.Title;
            //    ViewBag.Id = movieSummary.Id;
            //    return View();
            //}
            //else
            //{
            //    return LocalRedirect(Url.Content("~/account/login"));
            //}
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Review(ReviewRequestModel reviewRequestModel, int movieId)
        {
            try
            {
                int userId = _currentLogedInUser.UserId;
                bool result = await _userService.ReviewMovie(reviewRequestModel, userId, movieId);
                if (result)
                    return View("ReviewSuccess");
                return View("ReviewFail");
            }
            catch(Exception e)
            {
                return View("ReviewFail");
            }
        }
    }
}
