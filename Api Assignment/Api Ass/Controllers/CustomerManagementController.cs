using Api_Ass.Conetext;
using Api_Ass.Model.RequestModel;
using Api_Ass.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api_Ass.Controllers
{
    [Route("api/customers")]
    [ApiController]
    public class CustomerManagementController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            var customers = Context.users.Where(a => a.Role == "Customer");
            return Ok(customers);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetCustomer([FromRoute] Guid id)
        {
            var user = Context.users.FirstOrDefault(a => a.Id == id);
            return Ok(user);
        }

        [HttpPost]
        public IActionResult AddCustomer([FromBody] RegisterRequest request)
        {
            if (!Context.users.Any(a => a.Email == request.Email))
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
            user.Email = request.Email;
            user.Password = request.Password;
            user.Name = request.Name;
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteOrder(Guid id)
        {
            var user = Context.users.FirstOrDefault(a => a.Id == id);
            Context.users.Remove(user);
            return Ok();
        }
    }
}
