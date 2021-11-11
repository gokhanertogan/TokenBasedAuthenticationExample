using System.Threading.Tasks;
using TokenBasedAuthentication.API.Domain.Model;
using TokenBasedAuthentication.API.Domain.Responses;

namespace TokenBasedAuthentication.API.Domain.Services
{
    public interface IProductService
    {
        Task<ProductListResponse> ListAsync();

        Task<ProductResponse> AddProduct(Product product);

        Task<ProductResponse> RemoveProduct(int productId);

        Task<ProductResponse> UpdateProduct(Product product,int productId);

        Task<ProductResponse> FindByIdAsync(int productId);
    }
}