using System;
using System.Collections.Generic;
using System.Text;

namespace MovieShop.Core.Models.Request
{
    public class PurchaseRequestModel
    {
        public int MovieId { get; set; }
        public int UserId { get; set; }
        public decimal Price { get; set; }
    }
}
