using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineShopping.Data.Repository;
using OnlineShopping.Helpers;
using OnlineShopping.Data.Models;
using Microsoft.AspNetCore.Authorization;
using OnlineShopping.Models;
using Microsoft.AspNetCore.Identity;

namespace OnlineShopping.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICartRepository _cartRepository;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public CartController(IProductRepository productRepository, ICartRepository cartRepository, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _productRepository = productRepository;
            _cartRepository = cartRepository;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            var model = new CartViewModel();
            var cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            var total = 0;
            if (cart != null) total = cart.Sum(item => item.Product.Price * item.Quantity);
            model.Cart = cart;
            model.Total = total;
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddToCart(int id, int quantity)
        {
            var product = await _productRepository.FindByIdAsync(id);
            var cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            if (cart == null)
            {
                cart = new List<Item>();
            }
            cart.Add(new Item { Id = cart.Count()+1, Product = product, Quantity = quantity });
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return Ok();
        }
        public IActionResult Remove(int id)
        {
            List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            int index = isExist(id);
            cart.RemoveAt(index);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("Index");
        }
        private int isExist(int id)
        {
            List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Product.Id.Equals(id))
                {
                    return i;
                }
            }
            return -1;
        }
        public IActionResult CheckOut()
        {
            return Redirect("/Cart");
        }
        [Authorize(Roles = "User,Admin")]
        [HttpPost]
        public async Task<IActionResult> CheckOut(CartViewModel model)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            await _cartRepository.CheckOutAsync(user, model.Cart);
            return Ok("Đặt hàng thành công!");
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Invoice(int id)
        {
            return View(await _cartRepository.FindByIdAsync(id));
        }
    }
}