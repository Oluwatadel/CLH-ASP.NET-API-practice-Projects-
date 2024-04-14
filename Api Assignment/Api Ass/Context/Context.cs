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
                Role = "Admin",
                Password = "123456"
            },
            
            new User{
                Name = "Ayo",
                Email = "ayo",
                Role = "Manager",
                Password = "123456"
            },
            
            new User{
                Name = "Dami",
                Email = "Manager",
                Password = "123456"
            }
        };
    
        public static List<Role> roles = new List<Role>()
        {
            new Role{
                Name = "Admin"
            },

            new Role
            {
                Name = "Manager"
            },
            
            new Role
            {
                Name = "Customer"
            }
        };


        public static List<Product> products = new List<Product>
        {
            new Product
            {
                Name = "Vant",
                price = 2000

            }
        };

        public static List<Order> orders = null;
    }
}
