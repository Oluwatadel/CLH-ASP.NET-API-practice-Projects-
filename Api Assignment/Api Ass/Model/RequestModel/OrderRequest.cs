﻿namespace Api_Ass.Model.RequestModel
{
    public class OrderRequest
    {
        public string ProductName { get; set; }
        public string EmailOfCustomer { get; set; }
        public int quantity { get; set; } 
        //public double Total{ get; set; }
    }
}
