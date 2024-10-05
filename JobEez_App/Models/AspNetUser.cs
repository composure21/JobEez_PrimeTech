using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace JobEez_App.Models;

public partial class AspNetUser : IdentityUser
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;

    // The following properties are inherited from IdentityUser
    // public string? UserName { get; set; }
    // public string? NormalizedUserName { get; set; }
    // public string? Email { get; set; }
    // public string? NormalizedEmail { get; set; }
    // public bool EmailConfirmed { get; set; }
    // public string? PasswordHash { get; set; }
    // public string? SecurityStamp { get; set; }
    // public string? ConcurrencyStamp { get; set; }
    // public string? PhoneNumber { get; set; }
    // public bool PhoneNumberConfirmed { get; set; }
    // public bool TwoFactorEnabled { get; set; }
    // public DateTimeOffset? LockoutEnd { get; set; }
    // public bool LockoutEnabled { get; set; }
    // public int AccessFailedCount { get; set; }

    //public virtual ICollection<AspNetUserLogin> AspNetUserLogins { get; set; } = new List<AspNetUserLogin>();
    //public virtual ICollection<AspNetUserToken> AspNetUserTokens { get; set; } = new List<AspNetUserToken>();
    //public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; } = new List<IdentityUserClaim<string>>();
    //public virtual ICollection<AspNetUserRole> AspNetUserRoles { get; set; } = new List<AspNetUserRole>(); // Ensure this is defined


    // Uncomment the line below if you have a User Claims collection
    // public virtual ICollection<AspNetUserClaim> AspNetUserClaims { get; set; } = new List<AspNetUserClaim>();

    // Uncomment and implement if needed
    // public virtual ICollection<AspNetRole> Roles { get; set; } = new List<AspNetRole>();
}

