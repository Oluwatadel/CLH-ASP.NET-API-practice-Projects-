using Api_Ass.Conetext;
using Api_Ass.Model;
using Api_Ass.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api_Ass.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/roles")]
    [ApiController]
    public class RoleManagementController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleManagementController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public IActionResult GetRoles()
        {
            var roles = _roleService.GetAllRoles();
            return Ok(roles);

        }
        
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetRoles(Guid id)
        {
            var role = _roleService.GetRole(id);
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
            var role = _roleService.GetRole(id);

            if (role != null)
            {
                role.Name = name;
                return Ok();
            }
            return NotFound();
        }
        
        
        [HttpDelete]
        //[Route("{id}")]
        public IActionResult DeleteRole([FromRoute] Guid id) 
        {
            _roleService.DeleteRole(id);
            return Ok();
        }
    }

}
