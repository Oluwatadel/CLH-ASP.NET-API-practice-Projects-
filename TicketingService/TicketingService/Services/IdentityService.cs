using TicketingService.Controllers;

namespace TicketingService.Services
{
    public class IdentityService
    {
        public bool IsCredentialsValid(UserDto user)
        {
            if (user == null || ! ((string.Equals(user.UserName, "admin") || (string.Equals(user.UserName, "bob"))) && string.Equals(user.Password, "mypassword")))
            {
                return false;
            }
            return true;
        }

        public List<string> GetRoles(string userName)
        {
            if(userName == "admin")
            {
                return ["manager", "ceo"];
            }
            else
            {
                return ["manager"];
            }
        }
    }

}
