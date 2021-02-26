using MovieShop.Core.Models.Response;
using MovieShop.Core.RepositoryInterfaces;
using MovieShop.Core.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieShop.Infrastructure.Services
{
    public class CastService : ICastService
    {
        private readonly ICastRepository _castRepository;
        public CastService(ICastRepository castRepository)
        {
            _castRepository = castRepository;
        }
        public async Task<CastResponseModel> GetCastById(int id)
        {
            var cast = await _castRepository.GetByIdAsync(id);
            var castResponseModel = new CastResponseModel
            {
                Id = cast.Id,
                Gender = cast.Gender,
                Name = cast.Name,
                TmdbUrl = cast.TmdbUrl,
                ProfilePath = cast.ProfilePath,
            };
            return castResponseModel;
        }
    }
}
