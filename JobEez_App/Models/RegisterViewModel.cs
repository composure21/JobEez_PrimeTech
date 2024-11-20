using System.ComponentModel.DataAnnotations;

namespace JobEez_App.Models
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)]
        [Compare("Password", ErrorMessage = "Password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Role { get; set; } // Employee or Employer
        [Required]
        public bool Agreement { get; set; }
    }
}
