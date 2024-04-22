namespace Api_Ass.Model.RequestModel
{
    public class ProductCreateModel
    {
        public string? Name { get; set; } = default!;
        public decimal price { get; set; } = default!;
        public int QuantityInStock { get; set; } = default!;
    }
}
