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
        [Route("{referenc}")]
        public IActionResult GetProduct([FromRoute] Guid referenc)
        {
            var product = Context.orders.FirstOrDefault(a => a.ReferenceNo == referenc);
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
            var order = new Order
            {
                Product = product,
                TotalPrice = product.price * request.quantity
            };
            return Ok(order);
        }

        [HttpPut]
        public IActionResult UpdateOrder(Guid orderReference, OrderRequest request)
        {
            var order = Context.orders.FirstOrDefault(a => a.ReferenceNo == orderReference);
            order.Product = Context.products.FirstOrDefault(a => a.Name == request.ProductName)!;
            order.TotalPrice = order.Product.price * request.quantity;
            return Ok(order.TotalPrice);
        }

        [HttpDelete]
        public IActionResult DeleteOrder(Guid orderReferenceNo)
        {
            var order = Context.orders.FirstOrDefault(a => a.ReferenceNo == orderReferenceNo);
            Context.orders.Remove(order);
            return Ok();
        }
    }
}
