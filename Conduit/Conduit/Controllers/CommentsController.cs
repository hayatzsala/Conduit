using AutoMapper;
using Conduit.Db;
using Conduit.Dto;
using Conduit.Model;
using Conduit.Service;
using Conduit.Service.passwordHasher;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Conduit.Controllers
{
    [Route("Api/[Controller]")]
    [ApiController]
    public class CommentsController: Controller
    {
        public readonly IArticlesRepositry _ArticlesRepositry;
        public IConfiguration _configuration;
        public IMapper _mapper;
        public IUserRepositry _userRepositry;
        public ICommentsRepositry _commentsRepositry;


        public CommentsController(IArticlesRepositry articlesRepositry, IConfiguration configuration, IMapper mapper, IUserRepositry userRepositry, ICommentsRepositry commentsRepositry)
        {
            _ArticlesRepositry = articlesRepositry;
            _configuration = configuration;
            _mapper = mapper;
            _userRepositry = userRepositry;
            _commentsRepositry = commentsRepositry;
        }

        [HttpPost("Comment/",Name ="CreateComment")]
        [Authorize]
        public async Task<IActionResult> CreateComment(CommentD Comment)

        {
            var CommentData=_mapper.Map<Comment>(Comment);
            var userID = Guid.Parse(AuthModel.UserId);
            var userCreate =await _commentsRepositry.CreateComment(CommentData, userID);

            if (userCreate)
            {
                return Ok(Comment);
            }           
            return BadRequest();
        }





        [HttpGet("/AllComments/",Name ="GetAlComments")]
        [Authorize]
        public async Task<IActionResult> GetAllComment(Guid ArticleId)
        {
            var comments = await _commentsRepositry.GetAllComments(ArticleId);

            if (comments != null)
            {
                return Ok(comments);
            }

            return BadRequest();
        }


        [HttpGet("Comment/", Name = "GetComment")]
        [Authorize]
        public async Task<IActionResult> ReadComment(Guid Comment, Guid ArticleId)
        {
            var comment = await _commentsRepositry.GetComment(Comment,ArticleId);

            if (comment != null)
            {
                return Ok(comment);
            }

            return BadRequest();
        }


        [HttpDelete("/Comment/", Name = "DeleteComment")]
        [Authorize]
        public async Task<IActionResult> DeleteComment(Guid commentId, Guid articleId)
        {
            var Deletedcomment =  await _commentsRepositry.DeleteComment(commentId,articleId);

            if (Deletedcomment)
            {
                return Ok(Deletedcomment);
            }

            return BadRequest();
        }


    }
}

