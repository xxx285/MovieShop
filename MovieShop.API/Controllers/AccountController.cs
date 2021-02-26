using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieShop.Core.Models.Request;
using MovieShop.Core.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("")] // this line can be ommited
        public async Task<IActionResult> RegisterUser([FromBody] UserRegisterRequestModel user)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var createdUser = await _userService.RegisterUser(user); // if the validation failed, it won't come here. Don't know why.
            if (createdUser != null)
            {
                return CreatedAtRoute("GetUser", new { id = createdUser.Id }, createdUser);
                //return CreatedAtAction(nameof(LoginAsync), "not important"); // have bugs, check link for this: https://stackoverflow.com/questions/59288259/asp-net-core-3-0-createdataction-returns-no-route-matches-the-supplied-values
            }
            else
                return StatusCode(500);
        }

        [HttpGet]
        [Route("{id:int}", Name ="GetUser")]
        public async Task<IActionResult> GerUserById(int id)
        {
            try
            {
                var user = await _userService.GetUserById(id);
                if (user != null)
                    return Ok(user);
                return NotFound();
            }
            catch(Exception _)
            {
                return StatusCode(500, _);
            }
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> EmailExists([FromQuery] string email) // param in the url must match this
        {
            var user = await _userService.GetUserByEmail(email);
            bool emailExists = (user == null ? false : true);
            return Ok(new { emailExists = emailExists });
        }

        [HttpPost]
        [Route("login")] // this is not case-sensitive
        public async Task<IActionResult> Login([FromBody] LoginRequestModel loginRequestModel) // the json from body is NOT case-sensitive
        {
            var loginResponseModel = await _userService.ValidateUser(loginRequestModel);
            if (loginResponseModel == null)
                return StatusCode(401); // Unauthorized
            return Ok(new { token ="this_is_a_tocken_to_be_implemented" });
        }
    }
}
