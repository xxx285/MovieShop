using System;
using System.Collections.Generic;
using System.Text;

namespace MovieShop.Core.Models.Response
{
    public class MovieSummaryResponseModel
    {
        // picture; Title; Price; Id
        public int Id { get; set; }
        public string Title { get; set; }
        public string PosterUrl { get; set; }
        public decimal? Price { get; set; }

    }
}
