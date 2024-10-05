using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;

namespace JobEez_App.Models;

public partial class AspNetUserLogin : IdentityUserLogin<string>
{
    public string LoginProvider { get; set; } = null!;

    public string ProviderKey { get; set; } = null!;

    public string? ProviderDisplayName { get; set; }

    public string UserId { get; set; } = null!;

    public virtual AspNetUser User { get; set; } = null!;
}
