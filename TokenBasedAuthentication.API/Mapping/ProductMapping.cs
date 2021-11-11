using AutoMapper;
using TokenBasedAuthentication.API.Domain.Model;
using TokenBasedAuthentication.API.Resources;

namespace TokenBasedAuthentication.API.Mapping
{
    public class ProductMapping :Profile
    {
        public ProductMapping()
        {
            CreateMap<ProductResource,Product>().ReverseMap();   
        }
    }
}