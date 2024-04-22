using Api_Ass.Conetext;
using Api_Ass.Model;

namespace Api_Ass.Service
{
    public class OrderService : IOrderService
    {
        private readonly IProductService _productService;

        public OrderService(IProductService productService)
        {
            _productService = productService;
        }

        public Order AddOrder(Guid id)
        {
            var order = Context.orders.FirstOrDefault(a => a.Id == id);
            if (order == null)
            {
                order = new Order
                {
                    CustomerId = id,
                };
                Context.orders.Add(order);
            }
            return order;

        }

        public Order GetOrder(Guid id)
        {
            var order = Context.orders.FirstOrDefault(a => a.Id == id);
            return order == null ? null : order;
        }

        public Order CalculateOrder(Guid id)
        {
            var order = Context.orders.FirstOrDefault(a => a.CustomerId == id);
            foreach(var item in order.orderItems) 
            {
                order.Total += _productService.GetProduct(item.ProductId).price * item.ProductQuantityPurchased;
            }
            return order;
        }
    }
}
