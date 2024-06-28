using H2H.Physiotherapy.Common.Models.Account;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace H2H.Physiotherapy.Common.Utilities
{
    public static class TokenHelper
    {


        public static string GenerateToken(UserModel userModel, string signingKey, int tokenExpiry)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userModel.UserId.ToString()),
                new Claim(ClaimTypes.Name,userModel.UserName),
                new Claim(ClaimTypes.Role, userModel.Code),
            };
            var token = new JwtSecurityToken(claims: claims,
                                             expires: DateTime.Now.AddHours(tokenExpiry),
                                             signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);

           
        }









    }
}
