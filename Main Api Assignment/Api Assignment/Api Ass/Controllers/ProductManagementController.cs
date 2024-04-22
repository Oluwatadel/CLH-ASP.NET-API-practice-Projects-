using Api_Ass.Conetext;
using Api_Ass.Model;
using Api_Ass.Model.RequestModel;
using Api_Ass.Service.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api_Ass.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductManagementController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductManagementController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var products = _productService.GetAllProduct();
            return Ok(products);
        }

        [HttpGet]
        [Route("{id}")]        
        public IActionResult GetProduct([FromRoute] Guid id)
        {
            var product = _productService.GetProduct(a => a.Id == id);
            return Ok(product);
        }

        [Authorize(Roles = "Manager, Admin")]
        [HttpPost]
        public IActionResult AddProduct([FromBody] ProductCreateModel productCreateModel)
        {
            Context.products.Add(new Product
            {
                Name = productCreateModel.Name,
                price = productCreateModel.price,
                QuantityInStock = productCreateModel.QuantityInStock,
            });
            return Ok();
        }

        [Authorize(Roles = "Manager, Admin")]
        [HttpPut]
        public IActionResult UpdateProduct(Guid id, string productName)
        {
            var product = Context.products.FirstOrDefault(a => a.Id == id);
            product!.Name = productName;
            return Ok();
        }


        [Authorize(Roles = "Manager, Admin")]
        [HttpDelete]
        public IActionResult DeleteProduct(Guid id)
        {
            _productService.DeleteProduct(id);
            return Ok();
        }
    }
}
