using Conduit.Db;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Conduit.Service
{
    public interface IAuthService
    {
        public List<Claim> GetClaim(User model);
        public Task<JwtSecurityToken> GetJwtSecurityToken(IConfiguration _configuration, List<Claim> claims);
    }
}
