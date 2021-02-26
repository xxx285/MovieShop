using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieShop.Core.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieMVC.Controllers
{
    public class AdminController : Controller
    {
        [HttpGet]
        [Authorize(Roles = "Admin, SuperAdmin")]
        public async Task<IActionResult> AddMovie()
        {
            return View();
        }

        // Admin can create Movie
        [HttpPost]
        [Authorize(Roles = "Admin, SuperAdmin")]
        public async Task<IActionResult> CreateMovie(MovieCreateRequestModel movieCreateRequestModel)
        {
            return View();
        }

        // Admin can edit Movie
        [HttpPut]
        [Authorize(Roles = "Admin, SuperAdmin")]
        public async Task<IActionResult> Edit()
        {
            return View();
        }
    }
}
