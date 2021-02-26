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
    public class CastController : ControllerBase
    {
        private readonly ICastService _castService;
        public CastController(ICastService castService)
        {
            _castService = castService;
        }
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetCastById(int id)
        {
            var cast = await _castService.GetCastById(id);
            if (cast == null)
                return NotFound();
            return Ok(cast);
        }
    }
}
