using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnlineShopping.Data.Repository;
using OnlineShopping.Models;

namespace OnlineShopping.Controllers
{
    public class HomeController : Controller
    {
        private ICategoryRepository _categoryRepository;
        private IProductRepository _productRepository;

        public HomeController(ICategoryRepository categoryRepository, IProductRepository productRepository)
        {
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
        
        }

        public async Task<IActionResult> Index()
        {
            var model = new HomeViewModel
            {
                categoriesList = await _categoryRepository.GetAll(),
                productsList = (await _categoryRepository.FindByIdAsync(1)).Products
            };
            return View(model);
        }
        public async Task<IActionResult> ItemList(int id)
        {
            var model = new HomeViewModel
            {
                categoriesList = await _categoryRepository.GetAll(),
                productsList = (await _categoryRepository.FindByIdAsync(id)).Products
            };
            return View("Index", model);
        }
        public async Task<IActionResult> ItemDetail(int id)
        {
            var model = new ProductDetailViewModel
            {
                categoriesList = await _categoryRepository.GetAll(),
                product = await _productRepository.FindByIdAsync(id)
            };

            return View(model);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}
