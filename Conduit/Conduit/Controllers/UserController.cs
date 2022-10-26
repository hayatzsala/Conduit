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
    public class UserController: Controller
    {
        public readonly IUserRepositry _UserRepositry;
        public IConfiguration _configuration;
        public IMapper _mapper;
        public IUserService _iuserService;


        public UserController(IUserRepositry userRepositry, IConfiguration configuration, IMapper mapper, IUserService iuserService)
        {
            _UserRepositry = userRepositry;
            _configuration = configuration;
            _mapper = mapper;
            _iuserService = iuserService;
        }

        [HttpGet("user/",Name ="GetUser")]
        [Authorize]
        public async Task<IActionResult> getUser()
        {
            var data= _iuserService.getTokenInformation();

            var user = await _UserRepositry.GetUserByEmail(data.EmailAddress);
            return Ok(user);

        }

        [HttpGet("Alluser/", Name = "GetAllUser")]
        [Authorize]
        public async Task<IActionResult> AllUser()
        {
            var user = await _UserRepositry.GetAllUser();

            return Ok(user);

        }

        [HttpPut("user/",Name ="UpdateUserData")]
        [Authorize]
        public async Task<IActionResult> UpdateUser(UserD user)
        {
            var dataToken = getTokenInformation();
           var userd = _mapper.Map<User>(user);

            var UserData = await _UserRepositry.updateUserData(userd, dataToken.Email);

            if (UserData)
            {
          
                return Ok("Updated Succefully");
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
                    Username = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Name)?.Value
                };

                return AuthModel;
            }
            return null;

        }
    }
}

