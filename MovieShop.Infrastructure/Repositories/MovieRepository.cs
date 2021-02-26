using Microsoft.EntityFrameworkCore;
using MovieShop.Core.Entities;
using MovieShop.Core.RepositoryInterfaces;
using MovieShop.Core.Exceptions;
using MovieShop.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShop.Infrastructure.Repositories
{
    public class MovieRepository : EfRepository<Movie>, IMovieRepository
    {
        public MovieRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }
        public async Task<IEnumerable<Movie>> GetTopRatedMovies()
        {
            //var movieIds = await _dbContext.Reviews.GroupBy(r => r.MovieId)
            //    .Select(r => new {
            //        MovieId = r.Key,
            //        AverageRating = r.Sum(x => x.Rating) / r.Count()
            //    }).OrderByDescending(x => x.AverageRating).Select(x => x.MovieId).Take(25).ToListAsync();

            var movieIds = await _dbContext.Reviews.GroupBy(r => r.MovieId)
                .OrderByDescending(x => x.Average(r => r.Rating)).Select(x => x.Key).Take(25).ToListAsync();

            // kinda like subquery
            var movies = new List<Movie>();
            foreach(var i in movieIds)
            {
                var cur = await GetByIdAsync(i);
                movies.Add(cur);
            }
            return movies;
        }
        public async Task<IEnumerable<Movie>> GetTopRevenueMovies()
        {
            return await _dbContext.Movies.OrderByDescending(m => m.Revenue).Take(25).ToListAsync();
        }
        public override async Task<Movie> GetByIdAsync(int id)
        {
            return await _dbContext.Movies.Include(m => m.MovieCasts).ThenInclude(m => m.Cast).Include(m => m.Genres).FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<IEnumerable<Movie>> GetMoviesByGenre(int genreId, int pageSize = 25, int page = 1)
        {
            // first count the total number of movies in this genre
            var totalMoviesCountByGenre =
                await _dbContext.Genres.Include(g => g.Movies).Where(g => g.Id == genreId).SelectMany(g => g.Movies).CountAsync();

            if (totalMoviesCountByGenre == 0)
            {
                throw new NotFoundException("No Movies found for this genre");
            }
            var movies = await _dbContext.Genres.Include(g => g.Movies).Where(g => g.Id == genreId)
                .SelectMany(g => g.Movies)
                .OrderByDescending(m => m.Revenue).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            return movies;
        }

        public async Task<IEnumerable<Movie>> GetMoviesByPage(int pageSize = 25, int page = 1)
        {
            var movies = await _dbContext.Movies.OrderBy(m => m.Id).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            return movies;
        }

        public async Task<IEnumerable<Review>> GetReviewsForMovie(int i)
        {
            return await _dbContext.Reviews.Where(r => r.MovieId == i).Include(r => r.User).Include(r => r.Movie).ToListAsync();
            
        }
    }
}
