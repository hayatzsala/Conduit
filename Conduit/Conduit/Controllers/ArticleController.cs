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
        public ConduitContext _context;


        public ArticlesController(IArticlesRepositry articlesRepositry, IConfiguration configuration, IMapper mapper, IUserRepositry userRepositry, ConduitContext context)
        {
            _ArticlesRepositry = articlesRepositry;
            _configuration = configuration;
            _mapper = mapper;
            _userRepositry = userRepositry;
            _context = context;

        }

        [HttpPost("Articles/")]
        [Authorize]
        public async Task<IActionResult> CreateArticle(ArticleD article)

        {
            var userID = Guid.Parse(AuthModel.UserId);
            var userCreate =await _ArticlesRepositry.CreateArticle(article, userID);

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

        [HttpGet("/allarticle/{pagenumber}")]
        [Authorize]
        public async Task<ActionResult<List<Article>>> getAllarticle(int pageNumber)
        {
            var articles = await _ArticlesRepositry.GetAllArticle();

            if (articles != null)
            {
                const double ArticlesNumberinPage = 3f;

                var pagecount = Math.Ceiling(_context.Articles.Count() / ArticlesNumberinPage);

                var article = await _ArticlesRepositry.GetAllArticlePaginated(pageNumber, ArticlesNumberinPage);

                return Ok(
                    new ArticleResponse
                    {
                        ArticlesList = article,
                        CurrentPage = pageNumber,
                        PagesCount = (int)pagecount
                    }
                    );
            }
            return NotFound();
        }


        [HttpGet("Articles/")]
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


        [HttpDelete("/Articles/")]
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

