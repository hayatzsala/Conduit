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
    public class FavouriteController: Controller
    {
        public readonly IArticlesRepositry _ArticlesRepositry;
        public IConfiguration _configuration;
        public IMapper _mapper;
        public IUserRepositry _userRepositry;
        public IFavouriteRepositry _FavouriteRepositry;

        public FavouriteController(IArticlesRepositry articlesRepositry, IConfiguration configuration, IMapper mapper, IUserRepositry userRepositry, IFavouriteRepositry favouriteRepositry)
        {
            _ArticlesRepositry = articlesRepositry;
            _configuration = configuration;
            _mapper = mapper;
            _userRepositry = userRepositry;
            _FavouriteRepositry = favouriteRepositry;
        }

        [HttpPost("Favourite/",Name ="AddFavourite")]
        [Authorize]
        public async Task<IActionResult> AddFavourite(Guid aricleId)

        {
            var data= getTokenInformation();
            var CommentData=_mapper.Map<Comment>(Comment);
            var userID = await _userRepositry.GetUserID(data.Email);
            var FavouriteCreate =await _FavouriteRepositry.AddFavourite(userID,aricleId);

            if (FavouriteCreate)
            {
                return Ok("Added Successfully !");
            }           
            return BadRequest();
        }

        private AuthModel getTokenInformation()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                var userClaims = identity.Claims;
                var AuthModel = new AuthModel
                {
                    Email = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value,
                   Username= userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Name)?.Value
            };

                return AuthModel;
            }
            return null;

        }


    }
}

