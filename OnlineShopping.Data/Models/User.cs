using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.Data.Models
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public string RoleId { get; set; }
        public virtual Role Role { get; set; }
    }
}
