using OnlineShopping.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.Models
{
    public class ProductDetailViewModel
    {
        public IEnumerable<Category> categoriesList { get; set; }
        public Product product { get; set; }
        public int Quantity { get; set; } = 0;
    }
}
