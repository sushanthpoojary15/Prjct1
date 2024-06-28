using H2H.Physiotherapy.Common.Utilities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2H.Physiotherapy.Common.Models.Account
{
    public class UserCredential
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        //[BindProperty(Name = "grant_type")]
        //[GrantType(ExpectedGrantType = "password")]
        //public string GrantType { get; set; }
    }

    public class RefreshTokenPost
    {
        public string Refreshtoken { get; set; }

        public string UserId { get; set; }
    }

    public class RefreshToken
    {
        public string Refreshtoken { get; set; }

        public string UserId { get; set; }
        public string UserName { get; set; }
        public string RoleCode { get; set; }

    }
}
