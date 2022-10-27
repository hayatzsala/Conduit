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
            var user = await _UserRepositry.GetUserByEmail(model.Email);

            if (user != null )
            {
                var HashPassword = _ipasswordHasher.VerifyPassword(model.Password,user.Password);
                if (HashPassword)
                {
                    var claims = _IAuthService.GetClaim(user);
                    var WrittenToken = await _IAuthService.GetJwtSecurityToken(_configuration, claims);
                    _IAuthService.readToken(WrittenToken);

                    return Ok(AuthModel.Token) ;
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
        public async Task<IActionResult> SignUp(User user)
        {
            var UserExist = await _UserRepositry.GetUserByEmail(user.Email);

            if (UserExist == null)
            {
                user.Password = _ipasswordHasher.HashPassword(user.Password);

                var createUser = await _UserRepositry.CreateUser(user);
                if (createUser)
                {
                    return Ok(
                        new User
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

