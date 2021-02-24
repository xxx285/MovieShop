using MovieShop.Core.Entities;
using MovieShop.Core.Models.Response;
using MovieShop.Core.RepositoryInterfaces;
using MovieShop.Core.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieShop.Infrastructure.Services
{
    public class MovieService : IMovieService
    {

        private readonly IMovieRepository _movieRepository; //Dependency Injection by constructor injection; why readonly: only change in declaration(inside constructor)

        public MovieService(IMovieRepository movieRepository) // Constructor with an Interface as parameter: pass a class that implement this Interface
        {
            _movieRepository = movieRepository; 
        }

        public async Task<MovieSummaryResponseModel> GetMovieSummaryById(int id)
        {
            var movie = await _movieRepository.GetByIdAsync(id);
            var movieBuy = new MovieSummaryResponseModel
            {
                Id = movie.Id,
                PosterUrl = movie.PosterUrl,
                Price = movie.Price,
                Title = movie.Title
            };
            return movieBuy;
        }

        public async Task<MovieDetailsResponseModel> GetMovieById(int id)
        {
            var movie = await _movieRepository.GetByIdAsync(id);

            // map movie entity to MovieDetailsResponseModel
            var movieDetails = new MovieDetailsResponseModel
            {
                Id = movie.Id,
                Title = movie.Title,
                Overview = movie.Overview,
                Tagline = movie.Tagline,
                Budget = movie.Budget,
                Revenue = movie.Revenue,
                ImdbUrl = movie.ImdbUrl,
                TmdbUrl = movie.TmdbUrl,
                PosterUrl = movie.PosterUrl,
                BackdropUrl = movie.BackdropUrl,
                OriginalLanguage = movie.OriginalLanguage,
                ReleaseDate = movie.ReleaseDate,
                RunTime = movie.RunTime,
                Price = movie.Price,
            };
            // map list of GenreModel
            List<GenreResponseModel> genreResponseModels = new List<GenreResponseModel>();
            foreach (var genre in movie.Genres)
            {
                var curGenreResponseModel = new GenreResponseModel
                {
                    Id = genre.Id,
                    Name = genre.Name
                };
                genreResponseModels.Add(curGenreResponseModel);
            }

            // map list of CastResponseModel
            List<CastResponseModel> castResponseModels = new List<CastResponseModel>();
            foreach (var moviecast in movie.MovieCasts)
            {
                var cast = moviecast.Cast;
                var curCastResponseModel = new CastResponseModel
                {
                    Id = cast.Id,
                    Name = cast.Name,
                    Gender = cast.Gender,
                    TmdbUrl = cast.TmdbUrl,
                    ProfilePath = cast.ProfilePath,
                    Character = moviecast.Character
                };
                castResponseModels.Add(curCastResponseModel);
            }

            movieDetails.Genres = genreResponseModels;
            movieDetails.Casts = castResponseModels;

            return movieDetails;
        }
        public async Task<IEnumerable<MovieCardResponseModel>> GetTop25GrossingMovies()
        {
            var movies = await _movieRepository.GetTopRevenueMovies();
            var movieCardResponseModel = new List<MovieCardResponseModel>();
            foreach (var movie in movies)
            {
                var movieCard = new MovieCardResponseModel
                {
                    Id = movie.Id,
                    PosterUrl = movie.PosterUrl,
                    Revenue = movie.Revenue,
                    Title = movie.Title
                };
                movieCardResponseModel.Add(movieCard);
            }
            return movieCardResponseModel;
        }
    }
}
