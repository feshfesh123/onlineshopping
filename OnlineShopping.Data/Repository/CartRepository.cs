using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineShopping.Data.Context;
using OnlineShopping.Data.Models;

namespace OnlineShopping.Data.Repository
{
    internal class CartRepository : ICartRepository
    {
        private readonly OnlineShoppingDbContext _dbContext;
        private readonly IProductRepository _productRepository;
        private readonly UserManager<User> _userManager;

        public CartRepository(OnlineShoppingDbContext dbContext, IProductRepository productRepository, UserManager<User> userManager)
        {
            _dbContext = dbContext;
            _productRepository = productRepository;
            _userManager = userManager;
        }
        public async Task<bool> CheckOutAsync(User user, List<Item> items)
        {
            var order = new Order { User = user, OrderProducts = new List<OrderProduct>() };
            var sum = 0;
            foreach( var item in items)
            {
                var product = await _productRepository.FindByIdAsync(item.Product.Id);
                var orderitem = new OrderProduct
                {
                    Price = product.Price,
                    Quanity = item.Quantity,
                    Total = product.Price * item.Quantity,
                    Product = product
                };
                sum += product.Price * item.Quantity;
                order.OrderProducts.Add(orderitem);
            }
            order.TotalPrice = sum;
            _dbContext.Orders.Add(order);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<Order> FindByIdAsync(int id)
        {
            return await _dbContext.Orders
                .Include(order => order.User)
                .Include(order => order.OrderProducts)
                .ThenInclude(orderProducts => orderProducts.Product)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Order>> GetAll()
        {
            return await _dbContext.Orders
                .Include(order => order.User).ToListAsync();
        }
        
    }
}
