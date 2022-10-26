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
        public IUserService _iuserService;

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
            var data = _iuserService.getTokenInformation();
            var userID = new Guid(data.Userid);       
                ///await _userRepositry.GetUserID(data.Email);
            var Follow =await _followRepositry.followAfriend(userID, FriendId);

            if (Follow)
            {
                return Ok("Followed !");
            }           
            return BadRequest();
        }

    }
}

