﻿using Api_Ass.Model;

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
                Email = "ayo",
                RoleId = Guid.Parse("702634f3-03e1-46b6-89f7-4f233d75feea"),
                Password = "123456"
            },
            
            new User{
                Name = "Dami",
                Email = "Manager",
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
            new Role{
                Name = "Admin",
                Id = Guid.Parse("b4a83ff8-f9ac-4e22-b945-f0b8845bcd46")

            },

            new Role
            {
                Name = "Manager",
                Id = Guid.Parse("702634f3-03e1-46b6-89f7-4f233d75feea")
                
            },
            
            new Role
            {
                Name = "Customer",
                Id = Guid.Parse("55ca7e16-bdd7-433e-b9df-0b809d42934e")

            },
            
            new Role
            {
                Name = "Salesperson",
                Id = Guid.Parse("55ca7e26-bdd7-433e-b9df-0b809d42934e")

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

        public static List<Order> orders = new List<Order>
        {
            new Order
            {
                EmailOfCustomer = "Manager",
                Product = "Vant",
                TotalPrice = 6000,
            }
        };
    }
}
