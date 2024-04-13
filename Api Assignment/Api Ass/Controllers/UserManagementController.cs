using Api_Ass.Conetext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api_Ass.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserManagementController : ControllerBase
    {
        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult GetAll()
        {
            var users = Context.users;
            return Ok(users);
        }
    }
}
