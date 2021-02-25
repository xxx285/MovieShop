using MovieShop.Core.Entities;
using MovieShop.Core.Models.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieShop.Core.ServiceInterfaces
{
    public interface IMovieService
    {
        Task<MovieDetailsResponseModel> GetMovieById(int id);
        Task<IEnumerable<MovieCardResponseModel>> GetMoviesByGenre(int genreId, int pageSize = 25, int page = 1);
        Task<IEnumerable<MovieCardResponseModel>> GetTop25GrossingMovies();
        Task<IEnumerable<MovieCardResponseModel>> GetMoviesByPage(int pageSize=25, int page=1);
        Task<IEnumerable<ReviewResponseModel>> GetReviewsForMovie(int id); // id is for movie's id
        Task<IEnumerable<MovieCardResponseModel>> GetTop25RatedMovies();
        Task<MovieSummaryResponseModel> GetMovieSummaryById(int id);
    }
}
