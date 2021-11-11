using AutoMapper;
using TokenBasedAuthentication.API.Domain.Model;
using TokenBasedAuthentication.API.Resources;

namespace TokenBasedAuthentication.API.Mapping
{
    public class ProductMapping
    {
        public ProductMapping()
        {
            CreateMap<ProductResource,Product>();
            CreateMap<Product,ProductResource>();    
        }
    }
}