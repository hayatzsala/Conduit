using AutoMapper;
using Conduit.Controllers;
using Conduit.Db;
using Conduit.Dto;
using Conduit.Model;
using Conduit.Service;
using Conduit.Service.passwordHasher;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;

namespace ConduitTesting.Controllers
{
    public class AuthControllerTest
    {
        public readonly IUserRepositry _UserRepositry;
        public IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IAuthService _IAuthService;
        private readonly IpasswordHasher _ipasswordHasher;


        public AuthControllerTest()
        {
            _UserRepositry = A.Fake<IUserRepositry>();
            _configuration = A.Fake<IConfiguration>();
            _mapper = A.Fake<IMapper>();
            _IAuthService = A.Fake<IAuthService>();
            _ipasswordHasher = A.Fake<IpasswordHasher>();
        }


        [Fact]///
        public async Task AuthControllerSignUp()
        {
            var userDto = A.Fake<UserD>();
            var user = A.Fake<User>();
            var userCreated = true;

            A.CallTo(() => _UserRepositry.GetUserByEmail(userDto.Email)).Returns(user);


            var controller = new AuthorizationController(_UserRepositry, _configuration, _mapper, _IAuthService, _ipasswordHasher);

            var result = await controller.SignUp(userDto);

            if (user == null)
            {
                A.CallTo(() => _ipasswordHasher.HashPassword(user.Password)).Returns(userDto.Password);

                A.CallTo(() => _UserRepositry.CreateUser(userDto)).Returns(userCreated);

                if (userCreated)
                {
                    result.GetType().Should().Be(typeof(OkObjectResult));
                    (result as OkObjectResult).StatusCode.Should().Be(200);
                }
                else
                {
                    result.GetType().Should().Be(typeof(BadRequestObjectResult));
                    (result as BadRequestObjectResult).StatusCode.Should().Be(400);

                }

            }
            else
            {
                result.GetType().Should().Be(typeof(ConflictResult));
                (result as ConflictResult).StatusCode.Should().Be(409);

            }
        }



        [Fact]
        public async Task AuthControllerSignIn()
        {
            var signInModel = A.Fake<SignInModel>();
            var claimsList = A.Fake<List<Claim>>();
            var user = A.Fake<User>();
            var userCreated = true;
            bool HashPassword=true;
            string writtenToken = "";
       
            A.CallTo(() => _UserRepositry.GetUserByEmail(signInModel.Email)).Returns(user);

            var controller = new AuthorizationController(_UserRepositry, _configuration, _mapper, _IAuthService, _ipasswordHasher);

            var result = await  controller.siginIn(signInModel);

            if (user != null)
            {
                A.CallTo(() => _ipasswordHasher.VerifyPassword(signInModel.Password,user.Password)).Returns(HashPassword);

                if (!HashPassword)
                {
                    A.CallTo(() => _IAuthService.GetClaim(user)).Returns(claimsList);
                    A.CallTo(() => _IAuthService.GetJwtSecurityToken(_configuration, claimsList)).Returns(writtenToken);
                    A.CallTo(() => _IAuthService.readToken(writtenToken));

                    result.GetType().Should().Be(typeof(OkObjectResult));
                    (result as OkObjectResult).StatusCode.Should().Be(409);
                }
                else
                {
                    result.GetType().Should().Be(typeof(ConflictResult));
                    (result as ConflictResult).StatusCode.Should().Be(409);
                }
                }
            else
            {
                result.GetType().Should().Be(typeof(BadRequestObjectResult));
                (result as BadRequestObjectResult).StatusCode.Should().Be(400);
            }



        }
    }
}
