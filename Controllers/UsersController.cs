using Microsoft.AspNetCore.Identity;
using Microsoft.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Linq;

namespace MvcMovie.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class UsersController : BaseController
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsersController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            ViewData["RoleManager"] = _roleManager; // Pasar RoleManager a la vista

            ViewBag.Genres = GetGenres();
            return View(users);
        }
        [HttpPost]
        public async Task<IActionResult> ChangeUserRole(string userId, string role)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                ViewBag.Genres = GetGenres();
                return NotFound();
            }

            var currentRoles = await _userManager.GetRolesAsync(user);
            var result = await _userManager.RemoveFromRolesAsync(user, currentRoles);
            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Error al quitar roles actuales.");
                // Manejar el error apropiadamente
            }

            result = await _userManager.AddToRoleAsync(user, role);
            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Error al agregar nuevo rol.");
                // Manejar el error apropiadamente
            }

            ViewBag.Genres = GetGenres();
            return RedirectToAction(nameof(Index));
        }
    }
}