using AutoMapper;
using Conduit.Controllers;
using Conduit.Db;
using Conduit.Dto;
using Conduit.Model;
using Conduit.Service;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;

namespace ConduitTesting.Controllers
{
    public class UserControllerTest
    {
        public readonly IUserRepositry _UserRepositry;
        public readonly IConfiguration _configuration;
        public IMapper _mapper;
        public IUserService _iuserService;
        public UserControllerTest()
        {
            _UserRepositry = A.Fake<IUserRepositry>();
            _configuration = A.Fake<IConfiguration>();
            _mapper = A.Fake<IMapper>();
            _iuserService = A.Fake<IUserService>();

        }

        [Fact]
        public async Task UserControllerGetUserReturnOk()
        {

            var user = A.Fake<User>();
            var email = "ali.x@gmail.com";
            A.CallTo(() => _UserRepositry.GetUserByEmail(email)).Returns(user);

            var controller = new UserController(_UserRepositry, _configuration, _mapper, _iuserService);

            var result = await controller.getUser();

            result.GetType().Should().Be(typeof(OkObjectResult));
            (result as OkObjectResult).StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task UserControllerGetAllUserReturnOk()
        {

            var user = A.Fake<ICollection<User>>();
            A.CallTo(() => _UserRepositry.GetAllUser()).Returns(user);

            var controller = new UserController(_UserRepositry, _configuration, _mapper, _iuserService);

            var result = await controller.AllUser();

            result.GetType().Should().Be(typeof(OkObjectResult));
            (result as OkObjectResult).StatusCode.Should().Be(200);

        }


        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task UserControllerUpdateUserReturnOk(bool updateStatus)
        {
            var user = A.Fake<User>();
            var userDto = A.Fake<UserD>();
            AuthModel.Email = "ali.x@gmail.com";
            A.CallTo(() => _mapper.Map<User>(userDto)).Returns(user);

            A.CallTo(() => _UserRepositry.updateUserData(user, AuthModel.Email)).Returns(updateStatus);

            var controller = new UserController(_UserRepositry, _configuration, _mapper, _iuserService);

            var result = await controller.UpdateUser(userDto);

            if (updateStatus)
            {
                result.GetType().Should().Be(typeof(OkResult));
                (result as OkResult).StatusCode.Should().Be(200);
            }

            else
            {
                result.GetType().Should().Be(typeof(BadRequestResult));
                (result as BadRequestResult).StatusCode.Should().Be(400);


            }

        }

    }
}
