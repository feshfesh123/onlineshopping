using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace OnlineShopping.Data.Models
{
    public class Role : IdentityRole
    {
        public IList<User> users {get; set;}
    }
}