using Api_Ass.Model;

namespace Api_Ass.Conetext
{
    public class Context
    {
        
        public static List<User> users = new List<User>()
        {
            new User{
                Name = "Tobi",
                Email = "Admin",
                RoleId = Guid.Parse("b4a83ff8-f9ac-4e22-b945-f0b8845bcd46"),
                Password = "123456"
            },

            new User{
                Name = "Ayo",
                Email = "Manager",
                RoleId = Guid.Parse("702634f3-03e1-46b6-89f7-4f233d75feea"),
                Password = "123456"
            },

            new User{
                Name = "Dami",
                Email = "Salesperson",
                RoleId = Guid.Parse("55ca7e26-bdd7-433e-b9df-0b809d42934e"),
                Password = "123456"
            },

            new User{
                Name = "Wadood",
                Email = "Customer",
                RoleId = Guid.Parse("55ca7e16-bdd7-433e-b9df-0b809d42934e"),
                Password = "123456"
            }
        };

        public static List<Role> roles = new List<Role>()
        { 
        };


        public static List<Product> products = new List<Product>
        {
            new Product
            {
                Id = Guid.Parse("55ca7e16-bdd7-433e-b9df-0b819d42934e"),
                Name = "Vant",
                price = 2000,
                QuantityInStock = 17

            }
        };
        
        public static List<OrderItem> orderItems = new List<OrderItem>
        {
            //new OrderItem
            //{
            //    OrderId = Guid.Parse("55ca7e16-bdd7-433e-b9df-0b809d42934e"),
            //    ProductId = Guid.Parse("55ca7e16-bdd7-433e-b9df-0b819d42934e"),
            //    ProductName = "Vant",
            //    ProductQuantityPurchased = 4
            //}
        };

        public static List<Order> orders = new List<Order>
        {
            //new Order
            //{
            //    CustomerId = Guid.Parse("55ca7e16-bdd7-433e-b9df-0b809d42934e"),
            //    orderItems = "Vant",
            //    TotalPrice = 6000,
            //}
        };
    }
}
