using OnlineShopping.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.Models
{
    public class CartViewModel
    {
        public List<Item> Cart { get; set; }
        public int Total { get; set; }
    }
}
