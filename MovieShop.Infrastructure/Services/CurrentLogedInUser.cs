using Microsoft.AspNetCore.Http;
using MovieShop.Core.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace MovieShop.Infrastructure.Services
{
    public class CurrentLogedInUser : ICurrentLogedInUser
    {
        // HttpContext class: it will give us all the info regarding that Http Request such as cookies, forms, method type, browser, user, IsAuthenticated, Claims

        private readonly IHttpContextAccessor _httpContextAccessor;
        public CurrentLogedInUser(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public bool IsAuthenticated => GetAuthenticated();
        private bool GetAuthenticated()
        {
            return _httpContextAccessor.HttpContext?.User.Identity != null && _httpContextAccessor.HttpContext != null &&
                  _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;
        }
        public string FullName => GetFullName();
        private string GetFullName()
        {
            var firstName = _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.GivenName).Value;
            var lastName = _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Surname).Value;
            return firstName + " " + lastName;
        }
        public string Email => throw new NotImplementedException();
        public List<string> Roles => throw new NotImplementedException();
        public bool IsAdmin => throw new NotImplementedException();
        public bool IsSuperAdmin => throw new NotImplementedException();
        public int UserId => GetUserId();
        private int GetUserId()
        {
            int userId = Convert.ToInt32(_httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            return userId;
        }
    }
}
