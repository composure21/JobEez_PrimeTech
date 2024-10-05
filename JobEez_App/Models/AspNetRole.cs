using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace JobEez_App.Models;

public partial class AspNetRole : IdentityRole<string>
{
    public string Id { get; set; } = null!;

    public string? Name { get; set; }

    public string? NormalizedName { get; set; }

    public string? ConcurrencyStamp { get; set; }

    //public virtual ICollection<AspNetRoleClaim> AspNetRoleClaims { get; set; } = new List<AspNetRoleClaim>();

    public virtual ICollection<AspNetUser> Users { get; set; } = new List<AspNetUser>();
    public virtual ICollection<IdentityRoleClaim<string>> Claims { get; set; } = new List<IdentityRoleClaim<string>>();
}
