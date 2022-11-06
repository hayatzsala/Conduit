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
    public class FollowControllerTest
    {

        public IConfiguration _configuration;
        public IUserRepositry _userRepositry;
        public IFollowRepositry _followRepositry;
        public IUserService _iuserService;
        public IMapper _mapper;

        public FollowControllerTest()
        {
            _configuration = A.Fake<IConfiguration>();
            _userRepositry = A.Fake<IUserRepositry>();
            _followRepositry = A.Fake<IFollowRepositry>();
            _mapper = A.Fake<IMapper>();
        }


        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task FollowControllerAddaFriend(bool followStatus)
        {
            AuthModel.UserId = "1A3B944E-3632-467B-A53A-206305310BAE";
            var userID = new Guid(AuthModel.UserId);
            var FriendId = new Guid("1A35944E-3632-467B-A53A-206305310BA1");

            A.CallTo(() => _followRepositry.followAfriend(userID, FriendId)).Returns(followStatus);

            var controller = new FollowController(_configuration, _mapper, _userRepositry, _followRepositry);

            var result = await controller.addAfreind(FriendId);

            if (followStatus)
            {
                result.GetType().Should().Be(typeof(OkObjectResult));
                (result as OkObjectResult).StatusCode.Should().Be(200);
            }
            else
            {
                result.GetType().Should().Be(typeof(BadRequestResult));
                (result as BadRequestResult).StatusCode.Should().Be(400);

            }
        }
    }
}
