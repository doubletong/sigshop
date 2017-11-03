using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIG.WebSiteWithIdentity.Services.Identity
{
    public class AppIdentityUser : IdentityUser
    {
        public DateTime Birthday { get; set; }
    }
}
