﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace JobEez_App.Models;

public partial class AspNetRoleClaim : IdentityRoleClaim<string>
{
    public int Id { get; set; }

    public string RoleId { get; set; } = null!;

    public string? ClaimType { get; set; }

    public string? ClaimValue { get; set; }

    //public virtual AspNetRole Role { get; set; } = null!;
}
