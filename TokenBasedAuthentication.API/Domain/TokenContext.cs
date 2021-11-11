using Microsoft.EntityFrameworkCore;
using TokenBasedAuthentication.API.Domain.Model;

namespace TokenBasedAuthentication.API.Domain
{
    public class TokenContext :DbContext
    {
        public TokenContext(DbContextOptions<TokenContext> options):base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        
    }
}