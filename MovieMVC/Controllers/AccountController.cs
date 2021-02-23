using Microsoft.AspNetCore.Mvc;
using MovieShop.Core.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieMVC.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Register()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterRequestModel requestModel)
        {
            // Model Binding
            if(!ModelState.IsValid) // a single validation's failing will invalidate the model status
            {
                // do something when validation failed
            }
            // send data to the DB
            return View();
        }
    }
}
