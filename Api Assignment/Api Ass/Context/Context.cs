using Api_Ass.Model;

namespace Api_Ass.Conetext
{
    public class Context
    {
        public static List<User> users = new List<User>()
        {
            new User{Name = "Tobi", Email = "Admin", Role = "Admin", Password = "123456"}
        };
    }
}
