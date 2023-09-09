using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace ExtRS.Portal.Models
{
    public class UserModel : IdentityUser
    {  
        public virtual string Email { get; set; } // example, not necessary
    }
}
