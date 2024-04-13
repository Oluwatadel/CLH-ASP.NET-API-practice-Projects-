using CustomerAPiPractice.Adapter;
using Microsoft.AspNetCore.Mvc;

namespace CustomerAPiPractice.Controllers
{
	[ApiController]
    [Route("api")]//[Route("api/[controller]")] //[Route([Controller])] 
    public class CustomerController : ControllerBase
    {
        private readonly JsonPlaceholderAdapter _jsonplaceholder;
        private static List<Customer> customers = new()
        {
            new Customer(){Id = 1, Name = "Ayo", Age = 10, City = "Abk"},
            new Customer(){Id = 2, Name = "Kayode", Age = 11, City = "Sag"},
            new Customer(){Id = 3, Name = "Jide", Age = 9, City = "Ifo"}
        };

        public CustomerController(JsonPlaceholderAdapter jsonPlaceholderAdapter)
        {
            _jsonplaceholder = jsonPlaceholderAdapter;
        }

        //[HttpGet]
        ///*[Route("GetCustomers")]*/  //This is an end point
        //public IActionResult GetCustomers()
        //{
        //    //var allCustomers = customers;
        //    if (!customers.Any())
        //    {
        //        return NotFound("Empty");
        //    }
        //    return Ok(customers);
        //}

        [HttpGet]
        [Route("{Id}")]
        /*[Route("GetCustomer/{Id}")]*/  //This is an endpoint
        public IActionResult GeCustomersById([FromRoute] int Id)
        {
            var customer = customers.FirstOrDefault(p => p.Id == Id);
            if (customer == null)
            {
                return NotFound("Customer does not exist");
            }
            return Ok(customer);
            
        }
        
        [HttpGet]
        //[Route("customers")] //This is an end point
        public IActionResult GetCustomers([FromQuery] string? name)
        {
            if(name == null)
            {
                return Ok(customers);
            }

            if(!customers.Select(p => p.Name).Any(p => p.Contains(name)))
            {
                return NotFound("Person not found");
            }
            var customer = customers.First(p => p.Name.Contains(name));

            return Ok(customer);
            
        }

        [HttpPost]
        public IActionResult CreateCustomer(Customer newCustomer)
        {
            
            customers.Add(newCustomer);
            if(!customers.Select(p => p.Name).Any(p => p.Contains(newCustomer.Name)))
            {
                return BadRequest("Something is wrong");
            }
            return Ok("Successfull");
        }


        [HttpDelete("{Id}")]
        //[Route ()]
        public IActionResult DeleteCustomer([FromRoute] int Id)
        {
            var customer = customers.FirstOrDefault(a => a.Id == Id);

            if (customer == null)
            {
                return BadRequest("THere is no customer with that id");
            }
            else
            {
                customers.Remove(customer);
                return Ok("Deleted successfully");
            }
        }


        //[HttpPost]
        //public IActionResult UpdateCustomer(int id, [FromBody] Customer customer)
        //{
        //    var existedCustomer = customers.FirstOrDefault(a => a.Id == id);
        //    if(existedCustomer == null)
        //    {
        //        return BadRequest("THere is no customer with that id");
        //    }
        //    customers.Add(customer);
        //    return Ok("Successfull");
        //}

       

       






        public class Customer
        {
            public int Id { get; set; } 
            public string Name { get; set; }
            public int Age { get; set; }
            public string City { get; set; }
        }



    }
}
