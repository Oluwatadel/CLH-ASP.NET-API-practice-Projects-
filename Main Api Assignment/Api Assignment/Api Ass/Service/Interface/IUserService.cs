using Api_Ass.Model;
using Api_Ass.Model.RequestModel;
using System.Collections.ObjectModel;

namespace Api_Ass.Service
{
    public interface IUserService
    {
        ICollection<User> GetAllUsers();
        User? GetUser(Func<User, bool> pred);
        User UpdateUser(Guid id, UpdateRequestModel request);
        void DeleteUser(Guid id);
        User AddUser(User user);
        ICollection<User> GetAllCustomers();
    }
}
