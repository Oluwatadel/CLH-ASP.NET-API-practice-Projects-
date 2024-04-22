using Api_Ass.Conetext;
using Api_Ass.Model;
using Api_Ass.Model.RequestModel;
using Api_Ass.Model.Viewmodel;
using Api_Ass.Service;
using Api_Ass.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api_Ass.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/users")]
    [ApiController]
    public class UserManagementController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserManagementController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAll()
        {
            var users = _userService.GetAllUsers();
            var userViews = users.Select(p => new UserViewModel
            {
                Name = p.Name,
                Email = p.Email,
                Role = p.Role.Name!,
                Id = p.Id,
            });
            return Ok(userViews);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Detail(Guid id)
        {
            var user = _userService.GetUser(a => a.Id == id);
            if (user == null)
            {
                return BadRequest();
            }
            var userView = new UserViewModel
            {
                Name = user.Name,
                Email = user.Email,
                Role = user.Role.Name!,
                Id = user.Id
            };
            return Ok(userView);
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Update(Guid id, UpdateRequestModel register)
        {
            var updateUser = _userService.UpdateUser(id, register);
            return Ok(new UserViewModel
            {
                Name = updateUser.Name,
                Email = updateUser.Email,
                Role = updateUser.Role.Name!,
                Id = updateUser.Id
            });
        }

        [HttpDelete]
        //[Route("{id}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            _userService.DeleteUser(id);
            return Ok();
        }


    }
}
