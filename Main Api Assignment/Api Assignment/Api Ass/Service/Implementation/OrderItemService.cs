using Api_Ass.Conetext;
using Api_Ass.Model;
using Api_Ass.Model.RequestModel;

namespace Api_Ass.Service.Implementation
{
    public class OrderItemService : IOrderItemService
    {
        public Order AddOrderItem(Guid CustomerId, OrderRequest request)
        {
            var product = Context.products.FirstOrDefault(a => a.Name == request.ProductName);
            if (product == null)
            {
                return null;
            }
            var order = Context.orders.FirstOrDefault(a => a.CustomerId == CustomerId);
            if (order == null)
            {
                order = new Order
                {
                    CustomerId = CustomerId,
                };
            }

            if (product.QuantityInStock < request.quantity)
            {
                return null;
            }

            var item = new OrderItem
            {
                OrderId = order.Id,
                ProductId = product.Id,
                ProductName = product.Name,
                ProductQuantityPurchased = request.quantity
            };
            Context.orders.Add(order);
            order.orderItems.Add(item);
            return order;
        }
    }
}
