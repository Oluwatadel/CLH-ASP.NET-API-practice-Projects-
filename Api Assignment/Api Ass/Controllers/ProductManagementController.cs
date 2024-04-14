using Api_Ass.Conetext;
using Api_Ass.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api_Ass.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductManagementController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            var products = Context.products;
            return Ok(products);
        }

        [HttpGet]
        [Route("{id}")]        
        public IActionResult GetProduct([FromRoute] Guid id)
        {
            var product = Context.products.FirstOrDefault(a => a.Id == id);
            return Ok(product);
        }

        [HttpPost]
        public IActionResult AddProduct([FromBody] string productName)
        {
            Context.products.Add(new Product
            {
                Name = productName,
            });
            return Ok();
        }

        [HttpPut]
        public IActionResult UpdateProduct(Guid id, string productName)
        {
            var product = Context.products.FirstOrDefault(a => a.Id == id);
            product.Name = productName;
            return Ok();
        }
        
        [HttpDelete]
        public IActionResult DeleteProduct(Guid id)
        {
            var product = Context.products.FirstOrDefault(a => a.Id == id);
            Context.products.Remove(product);
            return Ok();
        }
    }
}
