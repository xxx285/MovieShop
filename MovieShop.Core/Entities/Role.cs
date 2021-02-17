using System;
using System.Collections.Generic;
using System.Text;

namespace MovieShop.Core.Entities
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Collection
        public ICollection<User> Users { get; set; }
    }
}
