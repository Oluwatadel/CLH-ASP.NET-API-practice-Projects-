using Api_Ass.Model;
using Api_Ass.Model.RequestModel;
using Microsoft.AspNetCore.Identity.Data;

namespace Api_Ass.Service
{
    public interface IIdentityService
    {
        //User IsValidCrendentials(LoginRequestModel loginRequest);
        User AuthenticateUser(LoginRequestModel loginrequest);
        string GenerateToken(User user);
    }
}
