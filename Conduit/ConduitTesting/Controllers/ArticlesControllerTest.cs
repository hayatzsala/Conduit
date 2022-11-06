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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConduitTesting.Controllers
{
    public class ArticlesControllerTest
    {

        public readonly IArticlesRepositry _ArticlesRepositry;
        public IConfiguration _configuration;
        public IMapper _mapper;
        public IUserRepositry _userRepositry;
        public IUserService _iuserService;
        public ConduitContext _context;

        public ArticlesControllerTest()
        {
            _ArticlesRepositry = A.Fake<IArticlesRepositry>();
            _configuration = A.Fake<IConfiguration>(); ;
            _mapper = A.Fake<IMapper>(); ;
            _userRepositry = A.Fake<IUserRepositry>();
            _context = A.Fake<ConduitContext>();

        }


        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task ArticlesControllerCreateArticle(bool ArticleStatus)
        {
            var articleDto = A.Fake<ArticleD>();
            var comments = A.Fake<Comment>();

            AuthModel.UserId = "1A3B944E-3632-467B-A53A-206305310BAE";
            var userID = new Guid(AuthModel.UserId);


            A.CallTo(() => _ArticlesRepositry.CreateArticle(articleDto, userID)).Returns(ArticleStatus);

            var controller = new ArticlesController(_ArticlesRepositry, _configuration, _mapper, _userRepositry, _context);


            var result = await controller.CreateArticle(articleDto);

            if (ArticleStatus)
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



        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task ArticlesControllerUpdateArticle(bool ArticleStatus)
        {

            var article = A.Fake<Article>();
            var articleD = A.Fake<ArticleD>();

            A.CallTo(() => _mapper.Map<Article>(articleD)).Returns(article);

            A.CallTo(() => _ArticlesRepositry.UpdateArticle(article)).Returns(ArticleStatus);

            var controller = new ArticlesController(_ArticlesRepositry, _configuration, _mapper, _userRepositry, _context);

            var result = await controller.UpdateArticle(articleD);

            if (ArticleStatus)
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

        [Fact]///
        public async Task ArticlesControllerReadArticle()
        {
            var article = new Article();
            var ArticleId = new Guid("1A3B944E-3632-467B-A53A-206305310BAE");

            A.CallTo(() => _ArticlesRepositry.GetArticleById(ArticleId)).Returns(article);

            var controller = new ArticlesController(_ArticlesRepositry, _configuration, _mapper, _userRepositry, _context);

            var result = await controller.ReadArticle(ArticleId);

            if (article != null)
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

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task ArticlesControllerDeleteArticle(bool deletedArticleStatus)
        {
            var ArticleId = new Guid("1A3B944E-3632-467B-A53A-206305310BAE");

            A.CallTo(() => _ArticlesRepositry.DeleteArticle(ArticleId)).Returns(deletedArticleStatus);

            var controller = new ArticlesController(_ArticlesRepositry, _configuration, _mapper, _userRepositry, _context);

            var result = await controller.DeleteArticle(ArticleId);

            if (deletedArticleStatus)
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
