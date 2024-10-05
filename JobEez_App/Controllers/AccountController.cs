using JobEez_App.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JobEez_App.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<AspNetUser> _signInManager;

        public AccountController(SignInManager<AspNetUser> signInManager)
        {
            _signInManager = signInManager;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "BuildResumes"); // Redirect to a successful landing page
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                }
            }
            return View("~/Areas/Identity/Pages/Account/Login.cshtml", model);
        }
    }
}
