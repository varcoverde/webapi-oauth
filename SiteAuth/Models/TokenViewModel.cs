﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SiteAuth.Models
{
    public class TokenViewModel
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
        public string Name { get; set; }
    }
}