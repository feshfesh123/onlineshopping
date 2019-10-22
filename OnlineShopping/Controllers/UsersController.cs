using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using OnlineShopping.Data.Repository;
using OnlineShopping.Models;

namespace OnlineShopping.Controllers
{
    public class UsersController : Controller
    {
        private IUserRepository _userRepository;
        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        
        public IActionResult Register()
        {
            return View(new LoginRegisterViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> Register(LoginRegisterViewModel form)
        {
            
            if (await _userRepository.CreateAsync(form.register)) return RedirectToAction("Index", "Home");
            else throw new Exception("This username have already used !");
        }
        public IActionResult Login()
        {
            return View("Users/Register.cshtml", new LoginRegisterViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginRegisterViewModel form)
        {
            var user = await _userRepository.CanSignInAsync(form.login);
            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Role, user.RoleId.ToString())
                };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(principal);
                return RedirectToAction("Index", "Roles");
            }
            else
            throw new Exception("Wrong username/password");
        }

    }
}