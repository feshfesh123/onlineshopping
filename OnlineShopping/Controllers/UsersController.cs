using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineShopping.Data.Models;
using OnlineShopping.Data.Repository;

namespace OnlineShopping.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private IUserRepository _userRepository;
        private IRoleRepository _roleRepository;
        public UsersController(IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _userRepository.GetAll());
        }
        public async Task<IActionResult> Details(int id)
        {
            return View(await _userRepository.FindByIdAsync(id));
        }
        public async Task<IActionResult> Edit(int id)
        {
            var user = await _userRepository.FindByIdAsync(id);
            ViewData["Role"] = new SelectList(await _roleRepository.GetAll(), "Id", "Type", user.Role.Id);
            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, User user)
        {
            if (ModelState.IsValid)
            {
                await _userRepository.UpdateAsync(user);
                return RedirectToAction("Index", "Users");
            }
            return View(user);
        }
        public async Task<IActionResult> Delete(int id)
        {
            await _userRepository.DeleteAsync(id);
            return RedirectToAction("Index", "Users");
        }
    }
}