using Conduit.Db;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Conduit.Service
{
    public class AuthService: IAuthService
    {

        public List<Claim> GetClaim(User model)
        {
            return new List<Claim>() {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(ClaimTypes.NameIdentifier, model.UserId.ToString()),
                        new Claim(ClaimTypes.Name, model.UserName),
                        new Claim(ClaimTypes.Email, model.Email)
                    };
        }

        public async Task<JwtSecurityToken> GetJwtSecurityToken(IConfiguration _configuration ,List<Claim> claims)
        
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            return new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddDays(double.Parse(_configuration["Jwt:Duration"])),
                        signingCredentials: signIn);

        }

    }
}



