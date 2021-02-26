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
        public List<string> Roles => GetRoles();
        private List<string> GetRoles()
        {
            var claims = _httpContextAccessor.HttpContext?.User.Claims;
            var roles = new List<string>();
            foreach(var claim in claims)
            {
                if (claim.Type == ClaimTypes.Role)
                    roles.Add(claim.Value);
            }
            return roles;
        }
        public bool IsAdmin => GetIsAdmin();
        private bool GetIsAdmin()
        {
            var roles = Roles;
            return roles.Any(r => r == "Admin");
        }
        public bool IsSuperAdmin => GetIsSuperAdmin();
        private bool GetIsSuperAdmin()
        {
            var roles = Roles;
            return roles.Any(r => r == "SuperAdmin");
        }
        public int UserId => GetUserId();
        private int GetUserId()
        {
            int userId = Convert.ToInt32(_httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            return userId;
        }
    }
}
