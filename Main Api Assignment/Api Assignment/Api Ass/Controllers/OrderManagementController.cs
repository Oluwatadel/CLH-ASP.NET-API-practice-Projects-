using Api_Ass.Conetext;
using Api_Ass.Model;
using Api_Ass.Model.RequestModel;
using Api_Ass.Model.Viewmodel;
using Api_Ass.Service.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Api_Ass.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrderManagementController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IOrderItemService _orderItemService;
        private readonly IProductService _productService;

        public OrderManagementController(IOrderService orderService, IOrderItemService orderItemService, IProductService productService)
        {
            _orderService = orderService;
            _orderItemService = orderItemService;
            _productService = productService;
        }

        [Authorize(Roles = "Manager, Admin, Salesperson")]
        [HttpGet]
        public IActionResult GetAll()
        {   
            var orders = _orderService.GetOrders();

            return Ok(orders);
        }

        [Authorize(Roles = "Manager, Admin, Salesperson")]
        [HttpGet]
        [Route("{reference}")]
        public IActionResult GetOrder([FromRoute] Guid reference)
        {
            var product = _orderService.GetOrder(reference);
            return Ok(product);
        }

        [Authorize(Roles = "Admin, Salesperson")]
        [HttpPost("orders")]
        public IActionResult CreateNewOrder(Guid id, [FromBody] OrderRequest request)
        {
            if(!Context.products.Any(a => a.Name == request.ProductName))
            {
                return BadRequest();
            }
            var product = Context.products.FirstOrDefault(a => a.Name == request.ProductName);
            var order = Context.orders.FirstOrDefault(a => a.CustomerId ==  id);
            if(order == null)
            {
                order = new Order
                {
                    CustomerId = id,
                };
            }
            if(request.quantity > product.QuantityInStock)
            {
                return NotFound($"Unit of product is not up to your request....{product.QuantityInStock} remaining");
            }
            product.QuantityInStock -= request.quantity;
            var orderItem = new OrderItem
            {
                ProductId = product.Id,
                ProductName = request.ProductName,
                OrderId = order.Id,
                ProductQuantityPurchased = request.quantity,
            }
            order.orderItems.Add(orderItem); return Ok(order);
        }


        [Authorize(Roles = "Admin, Salesperson")]
        [HttpPut]
        public IActionResult UpdateOrder(Guid orderReference, OrderRequest request)
        {
            var order = Context.orders.FirstOrDefault(a => a.reference == orderReference);
            foreach(var item in order.orderItems)
            {
                if(item.ProductName == request.ProductName)
                {
                    var product = _productService.GetProduct(a => a.Name == request.ProductName);
                    item.ProductName = product.Name;
                    item.ProductId = product.Id;
                }
            }
            order.Total = _orderService.CalculateOrder(order.Id);
            return Ok(order);
        }


        [Authorize(Roles = "Admin, Salesperson")]
        [HttpDelete]
        public IActionResult DeleteOrder(Guid orderReferenceNo)
        {
            var order = _orderService.GetOrder(orderReferenceNo);
            Context.orders.Remove(order);
            return Ok();
        }
    }
}
