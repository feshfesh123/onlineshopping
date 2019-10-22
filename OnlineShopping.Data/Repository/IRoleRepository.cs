using OnlineShopping.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopping.Data.Repository
{
    public interface IRoleRepository
    {
        Task<bool> CreateAsync(Role role);
        Task<bool> DeleteAsync(string id);
        Task<Role> FindByIdAsync(string roleId);
    }
}
