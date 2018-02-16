using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SiteAuth.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<string> Roles { get; set; }
        public string Email { get; set; }
    }
}