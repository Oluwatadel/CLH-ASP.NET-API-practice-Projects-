namespace Api_Ass.Model
{
    public class Order
    {
        public Guid Id = Guid.NewGuid();
        public Guid reference = Guid.NewGuid();
        public Guid CustomerId { get; set; }
        public ICollection<OrderItem> orderItems = new List<OrderItem>();
        public decimal Total { get; set; }
    }
}
