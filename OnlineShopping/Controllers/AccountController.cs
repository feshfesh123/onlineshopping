using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShopping.Data.Models;
using OnlineShopping.Data.Repository;
using OnlineShopping.Models;

namespace OnlineShopping.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        
        public IActionResult Register()
        {
            return View(new LoginRegisterViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> Register(LoginRegisterViewModel form)
        {
            if (ModelState.IsValid)
            {
                var user = new User { UserName = form.registerUsername, Name = form.registerName };
                var result = await _userManager.CreateAsync(user, form.registerPassword);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "User");
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

            }
            return View(form);
        }
        public IActionResult Login()
        {
            return View("Register", new LoginRegisterViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginRegisterViewModel form)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(form.loginUsername, form.loginPassword, false, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError(string.Empty, "Invalid Login");
                
            }
            return View("Register", form);
            //var user = await _userRepository.CanSignInAsync(form.login);
            //if (user != null)
            //{   
            //    var claims = new List<Claim>
            //    {
            //        new Claim(ClaimTypes.Name, user.Name),
            //        new Claim(ClaimTypes.Role, user.Role.Type)
            //    };
            //    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            //    var principal = new ClaimsPrincipal(identity);

            //    await HttpContext.SignInAsync(principal);
            //    return RedirectToAction("Index", "Home");
            //}
            //else
            //throw new Exception("Wrong username/password");
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}