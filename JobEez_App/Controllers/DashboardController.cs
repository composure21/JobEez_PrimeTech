using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobEez_App.Controllers
{
    [Authorize] // Restricts access to authenticated users
    public class DashboardController : Controller
    {
        [Authorize(Roles = "Employer")] // Employer-specific dashboard
        public IActionResult Employer()
        {
            return View();
        }

        [Authorize(Roles = "Employee")] // Employee-specific dashboard
        public IActionResult Employee()
        {
            return View();
        }

        [Authorize(Roles = "Administrator")] // Administrator-specific dashboard
        public IActionResult Admin()
        {
            return View();
        }
    }
}
