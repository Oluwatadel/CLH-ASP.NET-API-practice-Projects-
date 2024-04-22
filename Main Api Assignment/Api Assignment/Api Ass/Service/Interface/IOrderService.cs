using Api_Ass.Model;

namespace Api_Ass.Service.Implementation
{
    public interface IOrderService
    {
        public Order GetOrder(Guid id);
        public ICollection<Order> GetOrders();

        public Order AddOrder(Guid id);
        public decimal CalculateOrder(Guid id);
    }
}
