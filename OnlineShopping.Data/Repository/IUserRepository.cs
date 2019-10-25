using OnlineShopping.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopping.Data.Repository
{
    public interface IUserRepository
    {
        Task<bool> CreateAsync(User user);
        Task<bool> UpdateAsync(User user);
        Task<bool> DeleteAsync(int id);
        Task<User> FindByIdAsync(int userId);
        Task<User> CanSignInAsync(User user);
        Task<IEnumerable<User>> GetAll();
    }
}
