﻿namespace Api_Ass.Model
{
    public class User
    {
        public Guid Id = Guid.NewGuid();
        public string? Name { get; set; }
        public string Email { get; set; }
        public Guid RoleId { get; set; } 
        public string? Password;
    }
}
;