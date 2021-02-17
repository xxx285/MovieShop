using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieShop.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieMVC.Controllers
{
    public class GenresController : Controller
    {
        private readonly MovieShopDbContext _dbContext;
        public GenresController(MovieShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            // var genres = _dbContext.Genres.ToList(); // this is usnig EF directly

            return View();
        }
    }
}
