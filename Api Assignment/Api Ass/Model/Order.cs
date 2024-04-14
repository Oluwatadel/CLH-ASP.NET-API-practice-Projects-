namespace Api_Ass.Model
{
    public class Order
    {
        public Guid ReferenceNo = Guid.NewGuid();
        public Product Product { get; set; }
        public double TotalPrice { get; set; }
        //public string? EmailOfCustomer { get; set; }
    }
}
