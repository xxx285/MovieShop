﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
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
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestModel loginRequest, string returnUrl = null)
        {
            // shortcut for if returnUrl == null then we go to home page
            // otherwise go the retuenUrl
            returnUrl ??= Url.Content("~/");

            if (!ModelState.IsValid)
            {
                return View();
            }
            var user = await _userService.ValidateUser(loginRequest);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Please check username and password");
                return View();
            }
            // if username/password is success
            // Cookie based Authentication
            // Create a cookie with some information such that id, firstname, lastname, roles etc. CLAIMS
            // that information should not be in plain text, it should be encrypted
            // send this loginRequest to the UserService that will validate the un/pw
            var roleString = user.Roles.ToString();
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName),
                new Claim(ClaimTypes.DateOfBirth, user.DateOfBirth.Value.ToShortDateString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            };

            // AddRange is adding a list to the end of another list
            if (user.Roles != null) claims.AddRange(user.Roles.Select(role => new Claim(ClaimTypes.Role, role.Name)));

            // sign the cookie card
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            // create a cookie
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            return LocalRedirect(returnUrl);
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterRequestModel requestModel)
        {
            // Model Binding
            // Form, it will look for input elements names and if those names match with our Action menthod model
            // properties
            // then it will automatically map that data
            // a control with name=EMAIL "abc@abc.com"
            // UserRegisterRequestModel => Email
            if (!ModelState.IsValid) // a single validation's failing will invalidate the model status
            {
                // do something when validation failed
                return View();
            }
            // send data to the DB
            // only when every validaiton passes make sure you save to database
            // call our User Service to save to Db
            var createdUser = await _userService.RegisterUser(requestModel);
            if (createdUser != null)
            {
                return RedirectToAction("Login");
            }
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(); // this line deletes the cookie
            return RedirectToAction("Login");
        }
    }
}
