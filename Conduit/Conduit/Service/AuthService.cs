using Conduit.Db;
using Conduit.Model;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Conduit.Service
{
    public class AuthService : IAuthService
    {

        public List<Claim> GetClaim(User model)
        {
            return new List<Claim>() {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim("Id", model.UserId.ToString()),
                        new Claim(ClaimTypes.Name, model.UserName),
                        new Claim(ClaimTypes.Email, model.Email),
                        new Claim(ClaimTypes.NameIdentifier, model.UserId.ToString())

                    };
        }

        public async Task<string> GetJwtSecurityToken(IConfiguration _configuration, List<Claim> claims)

        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken(
                             _configuration["JWT:Issuer"],
                             _configuration["JWT:Audience"],
                             claims,
                             expires: DateTime.UtcNow.AddMinutes(double.Parse(_configuration["JWT:Duration"])),
                             signingCredentials: signIn);


            var Securitytoken = new JwtSecurityTokenHandler().WriteToken(token);
            return Securitytoken;

        }

        public void readToken(string Securitytoken)
        {
            var tokenRead = new JwtSecurityTokenHandler().ReadJwtToken(Securitytoken);
            AuthModel.Token = Securitytoken;
            AuthModel.Email = tokenRead.Claims.FirstOrDefault(c => c.Type == "email")?.Value;
            AuthModel.UserId = tokenRead.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        }
    }
}



