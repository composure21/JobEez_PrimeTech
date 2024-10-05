
using Microsoft.AspNetCore.Identity;

namespace JobEez_App.Models
{
    public class AspNetUserRole : IdentityUserRole<string>
    {
        public virtual AspNetUser User { get; set; }
        public virtual IdentityRole Role { get; set; }
    }
}
