using EmployeeManagement.App.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EmployeeManagement.App.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class RoleController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: /Role/AssignAdmin?email=someone@example.com
        public async Task<IActionResult> AssignAdmin(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest("Email is required.");
            }

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            // Ensure the "Admin" role exists.
            if (!await _roleManager.RoleExistsAsync("Admin"))
            {
                var createResult = await _roleManager.CreateAsync(new IdentityRole("Admin"));
                if (!createResult.Succeeded)
                {
                    ViewData["Message"] = "Unable to create Admin role.";
                    return View();
                }
            }

            var result = await _userManager.AddToRoleAsync(user, "Admin");
            if (result.Succeeded)
            {
                ViewData["Message"] = $"User {email} has been assigned the Admin role.";
            }
            else
            {
                ViewData["Message"] = "An error occurred while assigning the role.";
            }

            return View();
        }
    }
}


//using EmployeeManagement.App.Models;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;

//namespace EmployeeManagement.App.Controllers
//{
//    [Authorize(Roles = "SuperAdmin")]
//    public class RoleController : Controller
//    {
//        private readonly UserManager<ApplicationUser> _userManager;

//        public RoleController(UserManager<ApplicationUser> userManager)
//        {
//            _userManager = userManager;
//        }

//        // GET: /Role/AssignAdmin?email=someone@example.com
//        public async Task<IActionResult> AssignAdmin(string email)
//        {
//            if (string.IsNullOrEmpty(email))
//            {
//                return BadRequest("Email is required.");
//            }

//            var user = await _userManager.FindByEmailAsync(email);
//            if (user == null)
//            {
//                return NotFound("User not found.");
//            }

//            var result = await _userManager.AddToRoleAsync(user, "Admin");
//            if (result.Succeeded)
//            {
//                ViewData["Message"] = $"User {email} has been assigned the Admin role.";
//            }
//            else
//            {
//                ViewData["Message"] = "An error occurred while assigning the role.";
//            }
//            return View();
//        }
//    }
//}
