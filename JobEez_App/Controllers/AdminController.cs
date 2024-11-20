using JobEez_App.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JobEez_App.Controllers
{
    //Code Attribution: https://learn.microsoft.com/en-us/aspnet/web-api/overview/older-versions/using-web-api-1-with-entity-framework-5/using-web-api-with-entity-framework-part-3
    public class AdminController : Controller
    {
        private readonly UserManager<AspNetUser> _userManager;

        public AdminController(UserManager<AspNetUser> userManager)
        {
            _userManager = userManager;
        }
        [HttpPost]
        public async Task<IActionResult> ConvertUserToAdmin(string userId)
        {
            // Find the user by ID
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"User with ID {userId} not found.");
            }

            // Check if the user is already in the Administrator role
            var isInRole = await _userManager.IsInRoleAsync(user, "Administrator");
            if (isInRole)
            {
                return BadRequest("User is already an Administrator.");
            }

            // Assign the Administrator role
            var result = await _userManager.AddToRoleAsync(user, "Administrator");
            if (result.Succeeded)
            {
                return Ok("User has been successfully converted to Administrator.");
            }

            return BadRequest("Failed to assign Administrator role.");
        }
    }
}
