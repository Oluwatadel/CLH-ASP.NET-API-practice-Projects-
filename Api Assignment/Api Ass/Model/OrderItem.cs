namespace Api_Ass.Model
{
    public class OrderItem
    {
        public Guid Id = Guid.NewGuid();
        public Guid ProductId { get; set; }
        public Guid OrderId { get; set; }
        public string? ProductName { get; set; }
        public int ProductQuantityPurchased {  get; set; }

    }
}
