using AutoMapper;
using Conduit.Db;
using Conduit.Dto;

namespace Conduit.Model
{
    public class ProfileMapper : Profile
    {
        public ProfileMapper()
        {
            CreateMap<User, UserD>();
            CreateMap<UserD, User>();
            CreateMap<ArticleD, Article>();
            CreateMap<Article, ArticleD>();
            CreateMap<Favourite, FavouriteD>();
            CreateMap<FavouriteD,Favourite>();
        }
    }
}
