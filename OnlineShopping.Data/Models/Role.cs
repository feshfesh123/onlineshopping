using System.Collections.Generic;

namespace OnlineShopping.Data.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public virtual List<User>  Users { get; set; }
    }
}