using Api_Ass.Conetext;
using Api_Ass.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api_Ass.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/roles")]
    [ApiController]
    public class RoleManagementController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetRoles()
        {
            var roles = Context.roles;
            return Ok(roles);

        }
        
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetRoles(Guid id)
        {
            var role = Context.roles.FirstOrDefault(a => a.Id == id);
            if (role == null)
            {
                return NotFound();
            }
            return Ok(role);
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult UpdateRole(Guid id, string name) 
        {
            var role = Context.roles.FirstOrDefault(x => x.Id == id);
            if (role != null)
            {
                role.Name = name;
                return Ok();
            }
            return NotFound();
        }
        
        
        [HttpDelete]
        //[Route("{id}")]
        public IActionResult UpdateRole([FromRoute] Guid id) 
        {
            var role = Context.roles.FirstOrDefault(x => x.Id == id);
            if (role == null)
            {
                return NotFound();
            }
            Context.roles.Remove(role);
            return Ok();
        }
    }

}
