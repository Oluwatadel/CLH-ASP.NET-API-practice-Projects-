using Api_Ass.Conetext;
using Api_Ass.Model;
using Api_Ass.Model.RequestModel;
using Api_Ass.Service;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Api_Ass.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IIdentityService _identityService;
        private IConfiguration _configure;

        public UserController(IIdentityService identityService, IConfiguration configure)
        {
            _identityService = identityService;
            _configure = configure;
        }


        /*[AllowAnonymous]*/ //This allow for bypassing of authentication
        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody] LoginRequestModel requestModel)
        {
            var user = _identityService.AuthenticateUser(requestModel);
            if (user == null)
            {
                return StatusCode(401);
            }

            var token = GenerateToken(user);
            return Ok(token);
        }

        //[HttpGet]
        //[Route("logout")]
        //public IActionResult Logout()
        //{

        //    HttpContext.Session.Remove("token");
        //    return Ok();

        //}

        [HttpPost]
        [Route("register")]
        public IActionResult Register(RegisterRequest register)
        {
            var userExist = Context.users.Any(a => a.Email == register.Email);
            if (userExist)
            {
                return BadRequest();
            }
            Context.users.Add(new User
            {
                Name = register.Name,
                Email = register.Email,
                Password = register.Password
            });
            return Ok();
        }


        private string GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configure["Jwt:Key"]));
            var crendentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);


            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role),
            };

            var claimIdentity = new ClaimsIdentity(claims);
            //var claimPrincipal = new ClaimsPrincipal(claimIdentity);
            //var claimProperty = new 
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimIdentity), new AuthenticationProperties());

            var token = new JwtSecurityToken(_configure["Jwt: Issuer"], _configure["Jwt: Audience"], claims,
                expires: DateTime.Now.AddMinutes(2),
                signingCredentials: crendentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        //private User GetCurrentUser()
        //{
        //    var identity = HttpContext.User.Identity as ClaimsIdentity;
        //    if(identity != null)
        //    {
        //        var userClaims = identity.Claims;
        //        return new User
        //        {
        //            Name = userClaims.FirstOrDefault(a => a.Type == ClaimTypes.Name)?.Value,
        //            Email = userClaims.FirstOrDefault(a => a.Type == ClaimTypes.Email)?.Value,
        //            Role = userClaims.FirstOrDefault(a => a.Type == ClaimTypes.Role)?.Value
        //        };
        //    }
        //    return null;
    }



}
