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
            var user = await _UserRepositry.GetUserByEmail(AuthModel.Email);
            return Ok(user);

        }

        [HttpGet("Alluser/", Name = "GetAllUser")]
        [Authorize]
        public async Task<IActionResult> AllUser()
        {
            var data = _iuserService.getTokenInformation();

            var user = await _UserRepositry.GetAllUser();

            return Ok(user);

        }

        [HttpPut("user/",Name ="UpdateUserData")]
        [Authorize]
        public async Task<IActionResult> UpdateUser(UserD user)
        {
           var userd = _mapper.Map<User>(user);

            var UserData = await _UserRepositry.updateUserData(userd, AuthModel.Email);

            if (UserData)
            {
          
                return Ok("Updated Succefully");
            }

            return BadRequest();
        }
    }
}

