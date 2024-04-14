using Api_Ass.Conetext;
using Api_Ass.Model;
using Api_Ass.Model.RequestModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Api_Ass.Service
{
    public class IdentityService : IIdentityService
    {
        private IConfiguration _configure;

        public IdentityService(IConfiguration configure)
        {
            _configure = configure;
        }


        private User IsValidCrendentials(LoginRequestModel loginRequest)
        {
            var user = Context.users.FirstOrDefault(a => a.Email == loginRequest.Email);
            if (user != null)
            {
                if (user.Password == loginRequest.Password)
                {
                    return user;
                }
                return null;
            }
            return null;
        }

        public User AuthenticateUser(LoginRequestModel loginrequest)
        {
            var user = IsValidCrendentials(loginrequest);
            if (loginrequest == null)
            {
                return null;
            }
            return user;
        }

    }
    
}
