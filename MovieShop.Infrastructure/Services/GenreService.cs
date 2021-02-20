using MovieShop.Core.Entities;
using MovieShop.Core.ServiceInterfaces;
using MovieShop.Core.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Text;
using MovieShop.Core.Models.Response;
using System.Threading.Tasks;

namespace MovieShop.Infrastructure.Services
{
    public class GenreService : IGenreService
    {
        private readonly IAsyncRepository<Genre> _genreRepository;
        public GenreService(IAsyncRepository<Genre> genreRepository)
        {
            _genreRepository = genreRepository;
        }
        public async Task<IEnumerable<GenreResponseModel>> GetAllGenres()
        {
            var genres = await _genreRepository.ListAllAsync();
            List<GenreResponseModel> genreResponseModels = new List<GenreResponseModel>();
            foreach (var genre in genres)
            {
                var curGenreResponseModel = new GenreResponseModel
                {
                    Id = genre.Id,
                    Name = genre.Name
                };
                genreResponseModels.Add(curGenreResponseModel);
            }
            
            return genreResponseModels;
        }
    }
}
