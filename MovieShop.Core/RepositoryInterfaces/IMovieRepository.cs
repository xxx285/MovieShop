using MovieShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieShop.Core.RepositoryInterfaces
{
    public interface IMovieRepository: IAsyncRepository<Movie>
    {
        Task<IEnumerable<Movie>> GetTopRatedMovies();
        Task<IEnumerable<Movie>> GetTopRevenueMovies();
        Task<IEnumerable<Movie>> GetMoviesByGenre(int genreId, int pageSize=25, int page=1);
        Task<IEnumerable<Movie>> GetMoviesByPage(int pageSize = 25, int page = 1);
        Task<IEnumerable<Review>> GetReviewsForMovie(int i);
    }
}
