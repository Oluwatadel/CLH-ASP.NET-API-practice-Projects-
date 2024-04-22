using Api_Ass.Model;
using Api_Ass.Model.RequestModel;

namespace Api_Ass.Service.Implementation
{
    public interface IProductService
    {
        public Product GetProduct(Func<Product, bool> pred);
        public ICollection<Product> GetAllProduct();
        public Product AddProduct(ProductCreateModel productCreate);
        public void DeleteProduct(Guid id);
        public Product UpdateProduct(Guid id, ProductCreateModel product);


    }
}
