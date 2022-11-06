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
        public IUserService _iuserService;

        public FavouriteController(IArticlesRepositry articlesRepositry, IConfiguration configuration, IMapper mapper, IUserRepositry userRepositry, IFavouriteRepositry favouriteRepositry)
        {
            _ArticlesRepositry = articlesRepositry;
            _configuration = configuration;
            _mapper = mapper;
            _userRepositry = userRepositry;
            _FavouriteRepositry = favouriteRepositry;
        }

        [HttpPost("Favourites/")]
        [Authorize]
        public async Task<IActionResult> AddFavourites(Guid aricleId)

        {
            var userID = new Guid(AuthModel.UserId);
            var FavouriteCreateStatus = await _FavouriteRepositry.AddFavourite(userID, aricleId);

            if (FavouriteCreateStatus)
            {
                return Ok("Added Successfully !");
            }           
            return BadRequest();
        }

    }
}

