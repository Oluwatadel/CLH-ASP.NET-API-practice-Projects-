using Api_Ass.Model;
using Api_Ass.Model.RequestModel;

namespace Api_Ass.Service.Implementation
{
    public interface IOrderItemService
    {
        public Order AddOrderItem(Guid customerId, OrderRequest request);

    }
}
