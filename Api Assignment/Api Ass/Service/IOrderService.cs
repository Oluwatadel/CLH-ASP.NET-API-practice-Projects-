using Api_Ass.Model;

namespace Api_Ass.Service
{
    public interface IOrderService
    {
        public Order GetOrder(Guid id);
        public Order AddOrder(Guid id);
        public Order CalculateOrder(Guid id);
    }
}
