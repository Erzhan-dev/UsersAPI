using Microsoft.AspNetCore.Mvc;
using UsersRESTAPI.Models;
using UsersRESTAPI.Repository;
using System.Threading.Tasks;
using System;
using UsersRESTAPI.Services;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace UsersRESTAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        readonly IUsersRepo userRe;
        public AuthController(IUsersRepo _user) => userRe = _user;
        [HttpPost]
        [Route("Sign")]
        public async Task<IActionResult> SignIn([FromBody] UserModelForAuth user)
        {
            string hashPass = userRe.PasswordComparison(user).Result;

             if (hashPass == null)
            {
                return BadRequest(new { errorText = "Invalid username or password." });
            }
            ClaimsIdentity identity = null;
            if (PasswordHashing.VerifyHashedPassword(hashPass.ToString(), user.Password) == true)
                identity = AuthOptions.GetIdentity(user);
            if (identity == null)
            {
                return BadRequest(new { errorText = "Invalid username or password." });
            }
            var response = new
            {
                access_token = GetJWT.GetJWTToken(identity),
                Lin = identity.Name
            };
            return Json(response);
        }
    }
}
