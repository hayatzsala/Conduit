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
                        new Claim("Id", model.UserId.ToString()),
                        new Claim(ClaimTypes.Name, model.UserName),
                        new Claim(ClaimTypes.Email, model.Email)
                    };
        }

        public async Task<JwtSecurityToken> GetJwtSecurityToken(IConfiguration _configuration ,List<Claim> claims)
        
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            return new JwtSecurityToken(
                        _configuration["JWT:Issuer"],
                        _configuration["JWT:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddMinutes(double.Parse(_configuration["JWT:Duration"])),
                        signingCredentials: signIn);

        }

    }
}



