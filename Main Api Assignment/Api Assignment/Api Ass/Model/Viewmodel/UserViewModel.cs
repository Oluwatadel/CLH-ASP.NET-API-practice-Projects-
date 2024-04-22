namespace Api_Ass.Model.Viewmodel
{
    public class UserViewModel
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        //public Guid RoleId { get; set; } = Guid.Parse("55ca7e16-bdd7-433e-b9df-0b809d42934e");
        public string Role { get; set; }

        public string? Password;
    }
}
