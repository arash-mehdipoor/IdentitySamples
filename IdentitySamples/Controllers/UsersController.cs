using IdentitySamples.Models.AAA.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentitySamples.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsersController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        [Authorize]
        public IActionResult Index()
        {
            var users = _userManager.Users.ToList();
            return View(users);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserModel model)
        {
            if (ModelState.IsValid)
            {
                var identityUser = new ApplicationUser()
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    CodeMeli = model.CodeMeli
                };
                var result = await _userManager.CreateAsync(identityUser, model.Password);
                if (result.Succeeded)
                {
                    TempData["Message"] = "User Created";
                    return RedirectToAction("Index", "Users");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = "User Created";
            }
            else
            {
                TempData["FaildMessage"] = "Faild Delete";
            }
            return RedirectToAction("Index", "Users");
        }


        public async Task<IActionResult> EditUserRole(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var userRole = await _userManager.GetRolesAsync(user);

            var Roles = _roleManager.Roles.ToList();

            EditUserRoleViewModel viewModel = new EditUserRoleViewModel()
            {
                UserId = user.Id,
                UserName = user.UserName,
                UserRoles = userRole.ToList(),
                Roles = Roles
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditUserRole(string userId, List<string> roles)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var currentRole = await _userManager.GetRolesAsync(user);
            foreach (var role in currentRole)
            {
                if (!roles.Any(c => c == role))
                {
                    var removeRole = await _userManager.RemoveFromRoleAsync(user, role);
                }
            }
            foreach (var role in roles)
            {
                var isInRole = await _userManager.IsInRoleAsync(user, role);
                if (!isInRole)
                {
                    var addRole = await _userManager.AddToRoleAsync(user, role);
                }
            }
            return RedirectToAction("Index", "Users");
        }
    }
}
