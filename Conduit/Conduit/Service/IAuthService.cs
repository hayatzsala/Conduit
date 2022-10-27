using Conduit.Db;
using Conduit.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Conduit.Service
{
    public interface IAuthService
    {
        public List<Claim> GetClaim(User model);
        public Task<string> GetJwtSecurityToken(IConfiguration _configuration, List<Claim> claims);
        public void readToken(string Securitytoken);
    }
}
