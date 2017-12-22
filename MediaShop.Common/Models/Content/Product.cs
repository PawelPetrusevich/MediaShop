using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaShop.Common.Models.Content
{
    public class Product : Entity
    {
        public string ProductName { get; set; }
        
        public string Description { get; set; }

        public ProductType ProductType { get; set; }

        public decimal ProductPrice { get; set; }

        public bool IsPremium { get; set; }

        public bool IsFavorite { get; set; }
    }
}
