using System.Collections.Generic;
using System.Threading.Tasks;
using TokenBasedAuthentication.API.Domain.Model;

namespace TokenBasedAuthentication.API.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> ListAsync();

        Task AddProductAsync(Product product);

        Task RemoveProductAsync(int productId);

        void UpdateProductASync(Product product);

        Task<Product> FindByIdAsync(int productId);
    }
}