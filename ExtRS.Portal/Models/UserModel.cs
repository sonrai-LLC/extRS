using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace ExtRS.Portal.Models
{
    public class UserModel : IdentityUser
    {  
        public new virtual string? Email { get; set; }
    }
}
