using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MovieShop.Core.Models.Request
{
    public class UserRegisterRequestModel
    {
        // https://docs.microsoft.com/en-us/aspnet/core/mvc/models/validation?view=aspnetcore-5.0#built-in-attributes for reference data annotation
        [Required(ErrorMessage = "Email cannot be empty")]
        [StringLength(50)]
        [EmailAddress(ErrorMessage ="invalid format")]
        public string Email { get; set; }
        
        [Required(ErrorMessage ="password cannot be empty!")]
        // [StringLength(100, ErrorMessage ="The {0} must be at least {2} characters long", MinimumLength=8)]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[#$^+=!*()@%&]).{8,}$", ErrorMessage = "password must contains at least one upper-case letter, one lower-case letter and one special character with total length between 8 and 100")] // 1 capital, small and special chars, 8 lenth, 100 max
        public string Password { get; set; }

        [Required(ErrorMessage = "Re-entered Password cannot be empty!")]
        [Compare("Password", ErrorMessage = "Password does not match")]
        public string RePassword { get; set; }

        [Required(ErrorMessage = "First Name cannot be empty")]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name cannot be empty")]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Date of Birth is required")] // Although it is RequiredAttribute by default
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
    }
}
