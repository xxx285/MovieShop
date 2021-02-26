using System;
using System.Collections.Generic;
using System.Text;

namespace MovieShop.Core.Models.Request
{
    public class GenreCheckboxRequestModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Checked { get; set; }
    }
}
