using Api_Ass.Conetext;
using Api_Ass.Model;
using Api_Ass.Model.RequestModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api_Ass.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrderManagementController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            var orders = Context.orders;
            return Ok(orders);
        }

        [HttpGet]
        [Route("reference")]
        public IActionResult GetProduct([FromRoute] Guid reference)
        {
            var product = Context.orders.FirstOrDefault(a => a.ReferenceNo == reference);
            return Ok(product);
        }

        [HttpPost]
        public IActionResult MakeOrder([FromBody] OrderRequest request)
        {
            if(!Context.products.Any(a => a.Name == request.ProductName))
                {
                return BadRequest();
            }
            var product = Context.products.FirstOrDefault(a => a.Name == request.ProductName);
            var Customer = Context.users.FirstOrDefault(a => a.Email == request.EmailOfCustomer);
            if(Customer == null)
            {
                return NotFound();
            }
            else
            {
                if(Customer.Role != "Customer") return BadRequest();
                var order = new Order
                {
                    Product = product.Name,
                    EmailOfCustomer = request.EmailOfCustomer,
                    quantity = request.quantity,
                    TotalPrice = product.price * request.quantity
                };
                Context.orders.Add(order);
                return Ok(order);
            }
            
        }

        [HttpPut]
        public IActionResult UpdateOrder(Guid orderReference, OrderRequest request)
        {
            var order = Context.orders.FirstOrDefault(a => a.ReferenceNo == orderReference);
            if(order != null)
            {
                return NotFound();
            }
            var product = Context.products.FirstOrDefault(a => a.Name == request.ProductName);
            if(product == null)
            {
                return NotFound();
            }
            order.Product = product.Name;
            order.TotalPrice = product.price * request.quantity;
            return Ok(order.TotalPrice);
        }

        [HttpDelete]
        public IActionResult DeleteOrder(Guid orderReferenceNo)
        {
            var order = Context.orders.FirstOrDefault(a => a.ReferenceNo == orderReferenceNo);
            Context.orders.Remove(order);
            return Ok();
        }

        [HttpGet]
        [Route("id/orders")]
        public IActionResult GetorderOfACustomer(Guid id)
        {
            var customer = Context.users.FirstOrDefault(a => a.Id == id);
            if(customer.Role != "Customer")
            {
                return BadRequest("User is not a customer");
            }
            var order = Context.orders.Where(a => a.EmailOfCustomer == customer.Email);
            return Ok(order);
        }
    }
}
