using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieMVC.Controllers
{
    public class AdminController : Controller
    {
        // Admin can create Movie
        [HttpPost]
        public IActionResult Create()
        {
            return View();
        }

        // Admin can edit Movie
        [HttpPut]
        public IActionResult Edit()
        {
            return View();
        }
    }
}
