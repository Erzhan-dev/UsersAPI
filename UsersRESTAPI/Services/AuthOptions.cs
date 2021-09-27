using Microsoft.IdentityModel.Tokens;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using UsersRESTAPI.Repository;
using UsersRESTAPI.Models;

namespace UsersRESTAPI.Services
{
    public class AuthOptions
    {
        public const string ISSUER = "MyAuthServer";
        public const string AUDIENCE = "MyAuthClient";
        const string KEY = "mysupersecret_secretkey!123"; 
        public const int LIFETIME = 1; 
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }

        public static ClaimsIdentity GetIdentity(UserModelForAuth user)
            
        {


                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.LIN)
                };
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
                                                                   "Token",
                                                                   ClaimsIdentity.DefaultNameClaimType,
                                                                   ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            
        }
    }
}
