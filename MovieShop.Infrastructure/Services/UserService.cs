using MovieShop.Core.Entities;
using MovieShop.Core.Exceptions;
using MovieShop.Core.Models.Request;
using MovieShop.Core.Models.Response;
using MovieShop.Core.RepositoryInterfaces;
using MovieShop.Core.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieShop.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMovieRepository _movieRepository;
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly ICryptoService _cryptoService;
        private readonly IReviewRepository _reviewRepository;
        public UserService(IUserRepository userRepository, ICryptoService cryptoService, IMovieRepository movieRepository,
            IPurchaseRepository purchaseRepository, IReviewRepository reviewRepository)
        {
            _userRepository = userRepository;
            _movieRepository = movieRepository;
            _cryptoService = cryptoService;
            _purchaseRepository = purchaseRepository;
            _reviewRepository = reviewRepository;
        }

        public async Task<bool> PurchaseMovie(PurchaseRequestModel purchaseRequestModel)
        {
            var purchase = new Purchase
            {
                MovieId = purchaseRequestModel.MovieId,
                UserId = purchaseRequestModel.UserId,
                TotalPrice = purchaseRequestModel.Price,
                PurchaseDateTime = DateTime.Now,
                PurchaseNumber = Guid.NewGuid()
            };
            
            var createdPurchase = await _purchaseRepository.AddAsync(purchase);
            return createdPurchase != null;
        }

        public async Task<bool> RegisterUser(UserRegisterRequestModel userRegisterRequestModel)
        {
            // we need to check whether that email exists or not
            var dbUser = await _userRepository.GetUserByEmail(userRegisterRequestModel.Email);
            if (dbUser != null)
            {
                throw new ConflictException("Email already exists");
            }
            // first generate Salt
            var salt = _cryptoService.GenerateRandomSalt();
            var hashedPassword = _cryptoService.HashPassword(userRegisterRequestModel.Password, salt);
            // hash the password with salt and save the salt and hashed password to the Database
            var user = new User
            {
                Email = userRegisterRequestModel.Email,
                Salt = salt,
                HashedPassword = hashedPassword,
                FirstName = userRegisterRequestModel.FirstName,
                LastName = userRegisterRequestModel.LastName,
                DateOfBirth = userRegisterRequestModel.DateOfBirth
            };
            var createdUser = await _userRepository.AddAsync(user);
            if (createdUser != null && createdUser.Id > 0)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> ReviewMovie(ReviewRequestModel reviewRequestModel, int userId, int movieId)
        {
            var newReview = new Review
            {
                MovieId = movieId,
                UserId = userId,
                ReviewText = reviewRequestModel.ReviewText,
                Rating = reviewRequestModel.Rating
            };
            var result = await _reviewRepository.AddAsync(newReview);
            return result != null;
        }

        public async Task<LoginResponseModel> ValidateUser(LoginRequestModel loginRequestModel)
        {
            var dbUser = await _userRepository.GetUserByEmail(loginRequestModel.Email);
            if (dbUser == null)
            {
                return null;
            }
            var hashedPassword = _cryptoService.HashPassword(loginRequestModel.Password, dbUser.Salt);
            if (hashedPassword == dbUser.HashedPassword)
            {
                // User has entered correct password
                var loginResponse = new LoginResponseModel
                {
                    Id = dbUser.Id,
                    Email = dbUser.Email,
                    FirstName = dbUser.FirstName,
                    LastName = dbUser.LastName,
                    DateOfBirth = dbUser.DateOfBirth // DateTime cannot be convert to DateTime? implicitly but DateTime? can be convert to DateTime 
                };
                return loginResponse;
            }
            return null;
        }
    }
}