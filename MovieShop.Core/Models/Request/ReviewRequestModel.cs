using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MovieShop.Core.Models.Request
{
    public class ReviewRequestModel
    {
        public string ReviewText { get; set; }

        [Required(ErrorMessage = "Rating cannot be empty")]
        public decimal Rating { get; set; }
    }
}
