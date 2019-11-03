using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.Data.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int TotalPrice { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public virtual User User { get; set; }
        public virtual IList<OrderProduct> OrderProducts { get; set; }
    }
}