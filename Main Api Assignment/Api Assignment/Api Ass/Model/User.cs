namespace Api_Ass.Model
{
    public class User
    {
        public Guid Id = Guid.NewGuid();
        public string? Name { get; set; }
        public string Email { get; set; }
        public Guid RoleId { get; set; } = Guid.Parse("55ca7e16-bdd7-433e-b9df-0b809d42934e");
        public Role Role { get; set; }

        public string? Password;

    }
}
;