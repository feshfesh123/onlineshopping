using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineShopping.Data.Context;
using OnlineShopping.Data.Models;
using OnlineShopping.Data.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShopping.Data.DependencyInjection
{
    public static class OnlineShoppingExtensions
    {
        public static IServiceCollection AddOnlineShoppingData(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity();
            services.AddRepository();
            services.AddDbContext<OnlineShoppingDbContext>(
                options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            return services;
        }
        static void AddRepository(this IServiceCollection services)
        {
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
        }
        static void AddIdentity(this IServiceCollection services)
        {
            services.AddIdentity<User, Role>()
                    .AddEntityFrameworkStores<OnlineShoppingDbContext>();
            services.Configure<IdentityOptions>(options =>
                options.Password.RequireNonAlphanumeric = false
                );
        }
    }
}
