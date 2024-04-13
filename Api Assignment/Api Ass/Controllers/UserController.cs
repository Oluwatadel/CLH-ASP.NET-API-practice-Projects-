using Api_Ass.Conetext;
using Api_Ass.Model;
using Api_Ass.Model.RequestModel;
using Api_Ass.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        public UserController(IIdentityService identityService)
        {
            _identityService = identityService;
        }


        [AllowAnonymous] //This allow for bypassing of authentication
        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody] LoginRequestModel requestModel)
        {
            var user = _identityService.AuthenticateUser(requestModel);
            if(user == null)
            {
                return StatusCode(401);
            }

            var token = _identityService.GenerateToken(user);
            return Ok(token);
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Register(RegisterRequest register)
        {
            var userExist = Context.users.Any(a => a.Email == register.Email);
            if(userExist)
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

        

        


    }
}
