using AutoMapper;
using Conduit.Db;
using Conduit.Dto;
using Conduit.Model;
using Conduit.Service;
using Conduit.Service.passwordHasher;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Conduit.Controllers
{
    [Route("Api/[Controller]")]
    [ApiController]
    public class FollowController: Controller
    {
        public IConfiguration _configuration;
        public IUserRepositry _userRepositry;
        public IFollowRepositry _followRepositry;

        public FollowController(IConfiguration configuration, IMapper mapper, IUserRepositry userRepositry, IFollowRepositry followRepositry = null)
        {
            _configuration = configuration;
            _userRepositry = userRepositry;
            _followRepositry = followRepositry;
        }

        [HttpPost("Follow/",Name ="AddFavourite")]
        [Authorize]
        public async Task<IActionResult> addAfreind(Guid FriendId)

        {
            var data= getTokenInformation();
            var userID = await _userRepositry.GetUserID(data.Email);
            var Follow =await _followRepositry.followAfriend(userID, FriendId);

            if (Follow)
            {
                return Ok("Followed !");
            }           
            return BadRequest();
        }

        private AuthModel getTokenInformation()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                var userClaims = identity.Claims;
                var AuthModel = new AuthModel
                {
                    Email = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value,
                   Username= userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Name)?.Value
            };

                return AuthModel;
            }
            return null;

        }


    }
}

