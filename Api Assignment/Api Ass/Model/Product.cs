namespace Api_Ass.Model
{
    public class Product
    {
        public Guid Id = Guid.NewGuid();
        public string? Name { get; set; }
        public double price { get; set; }
    }
}
