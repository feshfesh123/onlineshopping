using OnlineShopping.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopping.Data.Repository
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAll();
        Task<bool> CreateAsync(Product product);
        Task<bool> DeleteAsync(int id);
        Task<bool> UpdateAsync(Product product);
        Task<Product> FindByIdAsync(int id);

    }
}
