namespace TokenBasedAuthentication.API.Domain.Repositories
{
    public class BaseRepository
    {
        protected readonly TokenContext _context;

        public BaseRepository(TokenContext context)
        {
            _context = context;
        }
    }
}