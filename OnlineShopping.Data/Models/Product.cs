using System.Collections.Generic;

namespace OnlineShopping.Data.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Discount { get; set; } = 0;
        public string Description { get; set; }
        public int Price { get; set; }
        public string Unit { get; set; }
        public string Image { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}