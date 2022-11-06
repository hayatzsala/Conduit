using AutoMapper;
using Conduit.Controllers;
using Conduit.Db;
using Conduit.Dto;
using Conduit.Model;
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
    public class CommentControllerTest
    {
        public readonly IArticlesRepositry _ArticlesRepositry;
        public IConfiguration _configuration;
        public IMapper _mapper;
        public IUserRepositry _userRepositry;
        public ICommentsRepositry _commentsRepositry;

       public CommentControllerTest()
        {

            _ArticlesRepositry = A.Fake<IArticlesRepositry>();
            _configuration = A.Fake<IConfiguration>(); ;
            _mapper = A.Fake<IMapper>(); ;
            _userRepositry = A.Fake<IUserRepositry>(); ;
            _commentsRepositry = A.Fake<ICommentsRepositry>(); ;
        }



        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task CommentControllerCreateComment(bool CommentStatus)
        {
            var commentDto = A.Fake<CommentD>();
            var comments = A.Fake<Comment>();

            AuthModel.UserId = "1A3B944E-3632-467B-A53A-206305310BAE";
            var userID = new Guid(AuthModel.UserId);

            A.CallTo(() => _mapper.Map<Comment>(commentDto)).Returns(comments);

            A.CallTo(() => _commentsRepositry.CreateComment(comments, userID)).Returns(CommentStatus);

            var controller = new CommentsController(_ArticlesRepositry, _configuration, _mapper, _userRepositry, _commentsRepositry);

            var result = await controller.CreateComment(commentDto);

            if (CommentStatus)
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
        public async Task UserControllerGetAllComments()
        {

            var comment = A.Fake<IEnumerable<Comment>>();
            var ArticleId = new Guid("1A3B944E-3632-467B-A53A-206305310BAE");

            A.CallTo(() => _commentsRepositry.GetAllComments(ArticleId)).Returns(comment);
            var controller = new CommentsController(_ArticlesRepositry, _configuration, _mapper, _userRepositry, _commentsRepositry);

            var result = await controller.GetAllComment(ArticleId);

            if (comment != null)
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
        public async Task UserControllerReadComment()
        {
            var comment = A.Fake<Comment>();
            var CommentId = new Guid("154B944E-3632-467B-A53A-206305310BfE");
            var ArticleId = new Guid("1A3B944E-3632-467B-A53A-206305310BAE");

            A.CallTo(() => _commentsRepositry.GetComment(CommentId, ArticleId)).Returns(comment);

            var controller = new CommentsController(_ArticlesRepositry, _configuration, _mapper, _userRepositry, _commentsRepositry);

            var result = await controller.ReadComment(CommentId, ArticleId);

            if (comment != null)
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
        ///[InlineData(false)]
        public async Task UserControllerDeleteComment(bool deletedComentStatus)
        {
            var comment = A.Fake<Comment>();
            var CommentId = new Guid("154B944E-3632-467B-A53A-206305310BfE");
            var ArticleId = new Guid("1A3B944E-3632-467B-A53A-206305310BAE");

            A.CallTo(() => _commentsRepositry.DeleteComment(CommentId, ArticleId)).Returns(deletedComentStatus);

            var controller = new CommentsController(_ArticlesRepositry, _configuration, _mapper, _userRepositry, _commentsRepositry);

            var result = await controller.ReadComment(CommentId, ArticleId);

            if (deletedComentStatus)
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
