using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace JobEez_App.Models;

public partial class AspNetUser : IdentityUser
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Role {  get; set; } = null!;  
    public bool HasPaid { get; set; }
    public string? PayFastPaymentId { get; set; }
    public DateTime? PaymentDate { get; set; }

}

