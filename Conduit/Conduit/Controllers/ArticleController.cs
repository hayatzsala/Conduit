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
    public class ArticlesController: Controller
    {
        public readonly IArticlesRepositry _ArticlesRepositry;
        public IConfiguration _configuration;
        public IMapper _mapper;
        public IUserRepositry _userRepositry;
        public IUserService _iuserService;
  

        public ArticlesController(IArticlesRepositry articlesRepositry, IConfiguration configuration, IMapper mapper,IUserRepositry userRepositry )
        {
            _ArticlesRepositry = articlesRepositry;
            _configuration = configuration;
            _mapper = mapper;
            _userRepositry = userRepositry;
          
        }

        [HttpPost("Article/",Name ="CreateArticel")]
        [Authorize]
        public async Task<IActionResult> CreateArticle(ArticleD article)

        {
            var data = _iuserService.getTokenInformation();
            var  artcleData=_mapper.Map<Article>(article);
            var userID = new Guid(data.Userid);
             ///await _userRepositry.GetUserID(data.Email);
            var userCreate =await _ArticlesRepositry.CreateArticle(artcleData,userID);

            if (userCreate)
            {
                return Ok(article);
            }           
            return BadRequest();
        }

        [HttpPut("Article/", Name = "UpdateArticle")]
        [Authorize]
        public async Task<IActionResult> UpdateArticle(ArticleD article)
        {
            var artcleData = _mapper.Map<Article>(article);
            var Article = await _ArticlesRepositry.UpdateArticle(artcleData);

            if (Article)
            {
                return Ok(Article);
            }

            return BadRequest();

        }

        [HttpGet("/AllArticles/",Name ="GetAllArticles")]
        [Authorize]
        public async Task<IActionResult> GetAllArticle()
        {
            var articles = await _ArticlesRepositry.GetAllArticle();

            if (articles!=null)
            {
                return Ok(articles);
            }

            return BadRequest();
        }

        [HttpGet("Article/", Name = "GetArticle")]
        [Authorize]
        public async Task<IActionResult> ReadArticle(Guid ArticleID)
        {
            var articles = await _ArticlesRepositry.GetArticleById(ArticleID);

            if (articles != null)
            {
                return Ok(articles);
            }

            return BadRequest();
        }


        [HttpDelete("/Article/", Name = "DeleteArticle")]
        [Authorize]
        public async Task<IActionResult> DeleteArticle(Guid ArticleID)
        {
            var articles =  await _ArticlesRepositry.DeleteArticle(ArticleID);

            if (articles)
            {
                return Ok(articles);
            }

            return BadRequest();
        }


    }
}

