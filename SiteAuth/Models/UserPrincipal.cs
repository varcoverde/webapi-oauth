using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace SiteAuth.Models
{
    public class UserPrincipal : GenericPrincipal
    {
        public UserPrincipal(IIdentity identity, string[] roles)
            : base(identity, roles)
        {
        }

        public string Email { get; set; }
    }
}