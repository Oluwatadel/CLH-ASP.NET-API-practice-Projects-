namespace Api_Ass.Model
{
    public class Order
    {
        public Guid ReferenceNo = Guid.NewGuid();
        public string? Product { get; set; }
        public int quantity { get; set; }
        public double TotalPrice { get; set; }
        public string? EmailOfCustomer { get; set; }
    }
}
