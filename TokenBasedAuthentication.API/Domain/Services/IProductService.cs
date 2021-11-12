using System.Collections.Generic;
using System.Threading.Tasks;
using TokenBasedAuthentication.API.Domain.Model;
using TokenBasedAuthentication.API.Domain.Responses;

namespace TokenBasedAuthentication.API.Domain.Services
{
    public interface IProductService
    {
        Task<BaseResponse<IEnumerable<Product>>> ListAsync();

        Task<BaseResponse<Product>> AddProduct(Product product);

        Task<BaseResponse<Product>> RemoveProduct(int productId);

        Task<BaseResponse<Product>> UpdateProduct(Product product,int productId);

        Task<BaseResponse<Product>> FindByIdAsync(int productId);
    }
}