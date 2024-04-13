using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TicketingService.Services;

namespace TicketingService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController (IdentityService identityService) : ControllerBase
    {
        [HttpPost]
        public IActionResult Login([FromBody] UserDto user)
        {
            //if(user == null || !(string.Equals(user.UserName,"admin") && string.Equals( user.Password, "mypassword")))
            //{
            //    return Unauthorized();
            //}
            //else
            //{
            //    HttpContext.Session.SetString("token", "123456789");

            //}

            if (!identityService.IsCredentialsValid(user))
            {
                return Unauthorized();
            }
            else
            {
                var claims = new List<Claim>();
                var userNameClaim = new Claim(ClaimTypes.Name, user.UserName);
                claims.Add(userNameClaim);
                var roles = identityService.GetRoles(user.UserName);
                foreach(var role in roles)
                {
                    claims.Add( new Claim(ClaimTypes.Role, role));
                }
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), new AuthenticationProperties());

            }

            return Ok();

        }
        [HttpGet]
        public IActionResult Logout()
        {
           
            HttpContext.Session.Remove("token");
            return Ok();

        }

    }

    public class UserDto 
    {
        public required string UserName { get; set; }
        public required string Password { get; set; }

    }
}
