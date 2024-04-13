using Api_Ass.Conetext;
using Api_Ass.Model;
using Api_Ass.Model.RequestModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
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

        public string GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configure["Jwt:Key"]));
            var crendentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_configure["Jwt: Issuer"], _configure["Jwt: Audience"], null,
                expires: DateTime.Now.AddMinutes(2),
                signingCredentials: crendentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
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
