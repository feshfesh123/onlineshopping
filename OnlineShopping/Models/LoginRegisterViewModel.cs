using OnlineShopping.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.Models
{
    public class LoginRegisterViewModel
    {
        public string loginUsername { get; set; }
        public string loginPassword { get; set; }
        public string registerUsername { get; set; }
        public string registerPassword { get; set; }
        public string registerName { get; set; }
    }
}
