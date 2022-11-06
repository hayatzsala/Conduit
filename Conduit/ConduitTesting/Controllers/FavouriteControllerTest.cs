using AutoMapper;
using Conduit.Controllers;
using Conduit.Db;
using Conduit.Model;
using Conduit.Service;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConduitTesting.Controllers
{
    public class FavouriteControllerTest
    {
        public readonly IArticlesRepositry _ArticlesRepositry;
        public IConfiguration _configuration;
        public IMapper _mapper;
        public IUserRepositry _userRepositry;
        public IFavouriteRepositry _FavouriteRepositry;
        public IUserService _iuserService;



        public FavouriteControllerTest()
        {
            _ArticlesRepositry = A.Fake<IArticlesRepositry>();
            _configuration = A.Fake<IConfiguration>();
            _mapper = A.Fake<IMapper>();
            _userRepositry = A.Fake<IUserRepositry>();
            _FavouriteRepositry = A.Fake<IFavouriteRepositry>();
        }




        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task FavouriteControllerAddFavourites(bool AddFavouriteStatus)
        {

            AuthModel.UserId = "1A3B944E-3632-467B-A53A-206305310BAE";
            var userID = new Guid(AuthModel.UserId);

            var articleCode = "105B944E-3452-467B-A53A-206305310BAE";
             var articleId = new Guid(articleCode);

            A.CallTo(() => _FavouriteRepositry.AddFavourite(userID,articleId)).Returns(AddFavouriteStatus);

            var controller = new FavouriteController(_ArticlesRepositry, _configuration, _mapper, _userRepositry, _FavouriteRepositry);

            var result = await controller.AddFavourites(articleId);
            if (AddFavouriteStatus)
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
