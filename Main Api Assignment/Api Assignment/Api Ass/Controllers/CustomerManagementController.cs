using Api_Ass.Conetext;
using Api_Ass.Model.RequestModel;
using Api_Ass.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Api_Ass.Service.Interface;
using Api_Ass.Service;
using Microsoft.AspNetCore.Authorization;
using Api_Ass.Model.Viewmodel;

namespace Api_Ass.Controllers
{
    [Authorize(Roles = "Admin, Manager")]
    [Route("api/customers")]
    [ApiController]
    public class CustomerManagementController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly IContextService _contextService;

        public CustomerManagementController(IUserService userService, IRoleService roleService, IContextService contextService)
        {
            _userService = userService;
            _roleService = roleService;
            _contextService = contextService;
        }

        [HttpGet]
        public IActionResult GetAllCustomers()
        {
            var users = _userService.GetAllUsers();
            return Ok(users.Where(p => p.Role.Name == "Customer").
                Select(p => new UserViewModel
                {
                    Name = p.Name,
                    Email = p.Email,
                    Role = p.Role.Name!,
                    Id = p.Id
                }));
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetCustomer([FromRoute] Guid id)
        {
            var customer = _userService.GetUser(a => a.Id == id);
            if(customer?.Role.Name != "Customer")
            {
                return NotFound();
            }
            return Ok(new UserViewModel
            {
                Name = customer.Name,
                Email = customer.Email,
                Role = customer.Role.Name!,
                Id = customer.Id
            });
        }

        [HttpPost]
        public IActionResult AddCustomer([FromBody] RegisterRequest request)
        {
            var user = new User
            {
                Email = request.Email!,
                Name = request.Name,
                Password = request.Password,
                
            };
            user.Role = _roleService.GetRole(user.RoleId);
            _userService.AddUser(user);
            return Ok(new UserViewModel
            {
                Name = user.Name,
                Email = user.Email,
                Role = user.Role.Name!,
                Id = user.Id
            });
        }

        [HttpPut]
        public IActionResult UpdateCustomer(Guid id, UpdateRequestModel request)
        {
            var user = Context.users.FirstOrDefault(a => a.Id == id);
            if (user == null)
            {
                return BadRequest();
            }
            user.Email = request.Email;
            user.Password = request.Password;
            user.Name = request.Name;
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteCustomer(Guid id)
        {
            var user = Context.users.FirstOrDefault(a => a.Id == id);
            Context.users.Remove(user);
            return Ok();
        }
    }
}
