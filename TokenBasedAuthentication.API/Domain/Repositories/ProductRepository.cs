using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TokenBasedAuthentication.API.Domain.Model;

namespace TokenBasedAuthentication.API.Domain.Repositories
{
    public class ProductRepository : BaseRepository, IProductRepository
    {
        public ProductRepository(TokenContext context) : base(context)
        {
        }

        public async Task AddProductAsync(Product product)
        {
            await _context.Products.AddAsync(product);
        }

        public async Task<Product> FindByIdAsync(int productId)
        {
            return await _context.Products.FindAsync(productId);
        }

        public async Task<IEnumerable<Product>> ListAsync()
        {
             return await _context.Products.ToListAsync();
        }

        public async Task RemoveProductAsync(int productId)
        {
            var product= await FindByIdAsync(productId);
            _context.Products.Remove(product);
        }

        public void UpdateProductASync(Product product)
        {
            _context.Products.Update(product);
        }
    }
}