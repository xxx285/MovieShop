using MovieShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieShop.Core.RepositoryInterfaces
{
    public interface IMovieRepository: IAsyncRepository<Movie>
    {
        IEnumerable<Movie> GetTopRatedMovies();
        IEnumerable<Movie> GetTopRevenueMovies();
    }
}
