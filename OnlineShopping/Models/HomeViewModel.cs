using OnlineShopping.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.Models
{
    public class HomeViewModel
    {
        public IEnumerable<Category> categoriesList { get; set; }
        public IEnumerable<Product> productsList { get; set; }
    }
}
