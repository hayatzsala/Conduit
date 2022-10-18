using AutoMapper;
using Conduit.Db;
using Conduit.Dto;

namespace Conduit.helper
{
    public class ProfileMapper : Profile
    {
        public ProfileMapper()
        {
            CreateMap<User, UserD>();
            CreateMap<UserD, User>();
        }
    }
}
