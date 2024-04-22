using Api_Ass.Conetext;
using Api_Ass.Model;
using Api_Ass.Model.RequestModel;

namespace Api_Ass.Service.Implementation
{
    public class ProductService : IProductService
    {
        public Product AddProduct(ProductCreateModel productCreate)
        {
            var product = GetProduct(a => a.Name == productCreate.Name);
            if (product != null)
            {
                return null;
            }
            product = new Product
            {
                Name = productCreate.Name,
                price = productCreate.price,
                QuantityInStock = productCreate.QuantityInStock,
            };
            Context.products.Add(product);
            return product;
        }

        public void DeleteProduct(Guid id)
        {
            var product = GetProduct(a => a.Id == id);
            if (product != null)
            {
                Context.products.Remove(product);
            }
        }

        public ICollection<Product> GetAllProduct()
        {
            var products = Context.products;
            return products;
        }

        public Product? GetProduct(Func<Product, bool> pred)
        {
            var product = Context.products.FirstOrDefault(pred);
            return product;
        }

        public Product UpdateProduct(Guid id, ProductCreateModel product)
        {
            var productToUpdate = GetProduct(a => a.Id == id);
            productToUpdate.Name = product.Name ?? productToUpdate.Name;
            if (productToUpdate.price == null)
            {
                productToUpdate.price = productToUpdate.price;
            }
            productToUpdate.price = product.price;
            productToUpdate.QuantityInStock = product.QuantityInStock;
            return productToUpdate;

        }
    }
}
