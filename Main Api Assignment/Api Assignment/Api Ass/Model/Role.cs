namespace Api_Ass.Model
{
    public class Role
    {
        public Guid Id = Guid.NewGuid();
        public string? Name {  get; set; }
        public ICollection<User> users {  get; set; }

    }
}
