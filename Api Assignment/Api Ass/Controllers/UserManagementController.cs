using Api_Ass.Conetext;
using Api_Ass.Model.RequestModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api_Ass.Controllers
{
    [Route("api/users")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class UserManagementController : ControllerBase
    {
        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public IActionResult GetAll()
        {
            //var context = HttpContext;
            var users = Context.users;
            return Ok(users);
        }

        [HttpGet("{id}")]
        //[Route]
        public IActionResult Detail([FromRoute] Guid id)
        {
            var user = Context.users.FirstOrDefault(x => x.Id == id);
            if (user == null)
            {
                return BadRequest();
            }
            return Ok(user);
        }

        [HttpPut("{id}")]
        //[Route]
        public IActionResult Update([FromRoute] Guid id, [FromForm] UpdateRequestModel register)
        {
            var user = Context.users.FirstOrDefault(x => x.Id == id);
            if (user == null)
            {
                return BadRequest();
            }
            user.Email = register.Email;
            user.Password = register.Password;
            user.Name = register.Name;
            return Ok(user);
        }

        [HttpDelete]
        //[Route("{id}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            var user = Context.users.FirstOrDefault(x => x.Id == id);
            if (user == null)
            {
                return BadRequest();
            }
            Context.users.Remove(user);
            return Ok();
        }


    }
}
