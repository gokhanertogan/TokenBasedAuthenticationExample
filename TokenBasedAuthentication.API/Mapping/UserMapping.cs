using AutoMapper;
using TokenBasedAuthentication.API.Domain.Model;
using TokenBasedAuthentication.API.Resources;

namespace TokenBasedAuthentication.API.Mapping
{
    public class UserMapping : Profile
    {
        public UserMapping()
        {
            CreateMap<UserResource,User>().ReverseMap();
        }
    }
}