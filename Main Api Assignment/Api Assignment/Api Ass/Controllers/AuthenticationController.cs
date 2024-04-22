using Api_Ass.Conetext;
using Api_Ass.Model;
using Api_Ass.Model.RequestModel;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Api_Ass.Service.Implementation;
using Api_Ass.Service;
using Api_Ass.Service.Interface;

namespace Api_Ass.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IIdentityService _identityService;
        private readonly IConfiguration _configure;
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly IContextService _contextService;

        public AuthenticationController(IIdentityService identityService, IConfiguration configure, IUserService userService, IRoleService roleService, IContextService contextService)
        {
            _identityService = identityService;
            _configure = configure;
            _userService = userService;
            _roleService = roleService;
            _contextService = contextService;
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromForm] LoginRequestModel requestModel)
        {
            _contextService.CreateRole();
            var user = _identityService.AuthenticateUser(requestModel);
            if (user == null)
            {
                return StatusCode(401);
            }

            var token = GenerateToken(user);
            return Ok(token);
        }


        [HttpPost]
        [Route("register")]
        public IActionResult Register(RegisterRequest register)
        {
            var userExist = _userService.GetUser(a => a.Email == register.Email);
            if (userExist != null)
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
            var role = _roleService.GetRole(user.RoleId);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Name!),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, role.Name!),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            };

            var claimIdentity = new ClaimsIdentity(claims);
            //var claimPrincipal = new ClaimsPrincipal(claimIdentity);
            //var claimProperty = new 
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimIdentity), new AuthenticationProperties());

            var token = new JwtSecurityToken(_configure["Jwt:Issuer"], _configure["Jwt:Audience"], claims,
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
