using System.Threading.Tasks;

namespace TokenBasedAuthentication.API.Domain.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TokenContext _context;

        public UnitOfWork(TokenContext context)
        {
            _context = context;
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Complete()
        {
             _context.SaveChanges();
        }
    }
}

