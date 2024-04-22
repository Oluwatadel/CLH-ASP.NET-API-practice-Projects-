using Api_Ass.Conetext;
using Api_Ass.Model.RequestModel;
using Api_Ass.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Api_Ass.Controllers
{
    [Route("api/customers")]
    [ApiController]
    [Authorize(Roles = "Manager, Admin")]

    public class CustomerManagementController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            var role = Context.roles.FirstOrDefault(a => a.Name == "Customer");
            var customers = Context.users.Where(a => a.RoleId == role.Id);
            return Ok(customers);
        }

        [HttpGet("{id}")]
        //[Route]
        public IActionResult GetCustomer([FromRoute] Guid id)
        {
            var role = Context.roles.FirstOrDefault(a => a.Name == "Customer");
            var user = Context.users.FirstOrDefault(a => a.Id == id);
            if (user.RoleId != role.Id) return BadRequest(user);
            return Ok(user);
        }

        [HttpPost]
        public IActionResult AddCustomer([FromBody] RegisterRequest request)
        {
            if (Context.users.Any(a => a.Email == request.Email))
            {
                return BadRequest();
            }

            Context.users.Add(new Model.User
            {
                Name = request.Name,
                Email = request.Email,
                Password = request.Password,
            });

            return Ok();
        }

        [HttpPut]
        public IActionResult UpdateCustomer(Guid id, UpdateRequestModel request)
        {
            var user = Context.users.FirstOrDefault(a => a.Id == id);
            if (user == null)
            {
                return BadRequest();
            }
            user.Email = request.Email ?? user.Email;
            user.Password = request.Password ?? request.Password;
            user.Name = request.Name ?? user.Name;
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCusomer([FromRoute]  Guid id)
        {
            var user = Context.users.Where(a => a.Role == "Customer").FirstOrDefault(a => a.Id == id);
            Context.users.Remove(user);
            return Ok();
        }

        [HttpGet("{id}/orders")]
        //[Route]
        public IActionResult CustomersOrder([FromRoute] Guid id)
        {
            var user = Context.users.Where(a => a.Role == "Customer").FirstOrDefault(a => a.Id == id);
            var orders = Context.orders.Where(a => a.EmailOfCustomer == user.Email);
            return Ok(orders);
        }
    }
}
