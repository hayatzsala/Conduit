using AutoMapper;
using Conduit.Db;
using Conduit.Dto;
using Conduit.Model;
using Conduit.Service;
using Conduit.Service.passwordHasher;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Conduit.Controllers
{
    [Route("Api/[Controller]")]
    [ApiController]
    public class AuthorizationController: Controller
    {
        public readonly IUserRepositry _UserRepositry;
        public IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IAuthService _IAuthService;
        private readonly IpasswordHasher _ipasswordHasher;

        public AuthorizationController(IUserRepositry userRepositry, IConfiguration configuration, IMapper mapper , IAuthService iAuthService, IpasswordHasher ipasswordHasher)
        {
            _UserRepositry = userRepositry;
            _configuration = configuration;
            _mapper = mapper;
            _IAuthService = iAuthService;
            _ipasswordHasher = ipasswordHasher;
        }

        [HttpPost("SignIn" , Name = "SignInUser")]
        public async Task<IActionResult> SiginIn([FromBody] SignInModel model)
        {
            var user = await _UserRepositry.GetUser(model.Email);

            if (user != null )
            {
                var HashPassword = _ipasswordHasher.VerifyPassword(model.Password,user.Password);
                if (HashPassword)
                {
                    var claims = _IAuthService.GetClaim(user);
                    var token = await _IAuthService.GetJwtSecurityToken(_configuration, claims);

                    return Ok(
                       new AuthModel
                       {
                           Email = user.Email,
                           Username = user.UserName,
                           IsAuthenticated = true,
                           Message = "SignInSucccefully",
                           Token = new JwtSecurityTokenHandler().WriteToken(token),
                           ExpiresOn = _configuration["Jwt:Duration"],
                       }
                        );
                }
                else
                {
                    return Conflict();
                }
            }
            else
            {
                return BadRequest("Invalid credentials");
            }
        }


        [HttpPost("SignUp", Name = "CreateUser")]
        public async Task<IActionResult> SignUp(UserD user)
        {
            var UserData = _mapper.Map<User>(user);
            var UserExist = await _UserRepositry.GetUser(UserData.Email);

            if (UserExist != null)
            {
                UserData.Password = _ipasswordHasher.HashPassword(UserData.Password);

                var createUser = await _UserRepositry.CreateUser(UserData);
                if (createUser)
                {
                    return Ok(
                        new UserD
                        {
                            Email = user.Email,
                            UserName = user.UserName,
                            Age = user.Age,
                            Password = user.Password,
                            Bio = user.Bio
                        }
                        );
                }

                return BadRequest("Try Again :( !");
            }
            return Conflict();

        }
    }
}

