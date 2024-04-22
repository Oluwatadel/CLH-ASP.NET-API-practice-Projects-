using Api_Ass.Conetext;
using Api_Ass.Model;
using Api_Ass.Model.RequestModel;
using Api_Ass.Service.Interface;
using Microsoft.Win32;
using System.Linq;

namespace Api_Ass.Service.Implementation
{
    public class UserService : IUserService
    {
        public User AddUser(User user)
        {
            Context.users.Add(user);
            return user;
        }

        public void DeleteUser(Guid id)
        {
            var user = Context.users.FirstOrDefault(x => x.Id == id);
            if (user != null)
            {
                Context.users.Remove(user);
            }
        }

        public User? GetUser(Func<User, bool> pred)
        {
            var user = Context.users.FirstOrDefault(pred);
            return user;
        }

        public User? UpdateUser(Guid id, UpdateRequestModel request)
        {
            var user = Context.users.FirstOrDefault(x => x.Id == id);
            if (user == null)
            {
                return null;
            }
            user.Email = request.Email! ?? user.Email;
            user.Password = request.Password ?? user.Password;
            user.Name = request.Name ?? user.Name;
            return user;
        }

        public ICollection<User> GetAllCustomers()
        {
            var users = Context.users.Where(a => a.Role.Name == "Customer");
            return users.ToList();
        }

        public ICollection<User> GetAllUsers()
        {
            return Context.users;
        }
    }
}
