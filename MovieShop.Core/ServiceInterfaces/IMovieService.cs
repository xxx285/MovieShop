using MovieShop.Core.Entities;
using MovieShop.Core.Models.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieShop.Core.ServiceInterfaces
{
    public interface IMovieService
    {
        MovieDetailsResponseModel GetMovieById(int id);
        IEnumerable<MovieCardResponseModel> GetTop25GrossingMovies();
    }
}
