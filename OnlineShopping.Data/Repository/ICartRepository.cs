using OnlineShopping.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopping.Data.Repository
{
    public interface ICartRepository
    {
        Task<bool> CheckOutAsync(User user, List<Item> items);
        Task<IEnumerable<Order>> GetAll();
        Task<Order> FindByIdAsync(int id);
    }
}
