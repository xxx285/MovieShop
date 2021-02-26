using MovieShop.Core.Models.Request;
using MovieShop.Core.Models.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieShop.Core.ServiceInterfaces
{
    public interface IUserService
    {
        Task<UserRegisterResponseModel> RegisterUser(UserRegisterRequestModel userRegisterRequestModel);
        Task<UserRegisterResponseModel> GetUserById(int id);
        Task<UserRegisterResponseModel> GetUserByEmail(string email);
        Task<LoginResponseModel> ValidateUser(LoginRequestModel loginRequestModel);
        Task<bool> PurchaseMovie(PurchaseRequestModel purchaseRequestModel);
        Task<bool> ReviewMovie(ReviewRequestModel reviewRequestModel, int userId, int movieId);
    }
}
