using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using OnlineShopping.Data.Context;
using OnlineShopping.Data.Models;

namespace OnlineShopping.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly OnlineShoppingDbContext _dbContext;
        public UserRepository(OnlineShoppingDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public static string MD5Hash(string input)
        {
            using (var md5 = MD5.Create())
            {
                var result = md5.ComputeHash(Encoding.ASCII.GetBytes(input));
                return Encoding.ASCII.GetString(result);
            }
        }
        public async Task<bool> CreateAsync(User user)
        {
            var role = _dbContext.Roles.Where(r => r.Type == "Admin").FirstOrDefault();
            user.Role = role;

            var res = _dbContext.Users.Where(x => x.Username == user.Username).FirstOrDefault();

            if (res == null && user.Password != null)
            {
                user.Password = MD5Hash(user.Password);
                _dbContext.Users.Add(user);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var user = await _dbContext.Users.FindAsync(id);
            if (user != null)
            {
                _dbContext.Users.Remove(user);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<User> FindByIdAsync(string userId)
        {
            return await _dbContext.Users.FindAsync(userId);
        }
        public async Task<User> CanSignInAsync(User user)
        {
            var res = _dbContext.Users.Where(x => x.Username == user.Username).FirstOrDefault();
            if ( res!= null && MD5Hash(user.Password) == res.Password) return res;
            return null;
        }
    }
}
