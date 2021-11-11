using System;
using System.Linq;
using Microsoft.Extensions.Options;
using TokenBasedAuthentication.API.Domain.Model;
using TokenBasedAuthentication.API.Security.Token;

namespace TokenBasedAuthentication.API.Domain.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        private readonly TokenOptions _tokenOptions;
        public UserRepository(TokenContext context, IOptions<TokenOptions> tokenOptions) : base(context)
        {
            _tokenOptions= tokenOptions.Value;
        }

        public void AddUser(User user)
        {
            _context.Users.Add(user);
        }

        public User FindByEmailAndPassword(string email, string password)
        {
            return _context.Users.Where(x => x.Email == email && x.Password == password).FirstOrDefault(); ;
        }

        public User FindById(int userId)
        {
            return _context.Users.Find(userId);
        }

        public User GetUserWithRefreshToken(string refreshToken)
        {
            return _context.Users.FirstOrDefault(x => x.RefreshToken == refreshToken);
        }

        public void RevokeRefreshToken(User user)
        {
            var newUser = _context.Users.Find(user.Id);
            newUser.RefreshToken = null;
        }

        public void SaveRefreshToken(int userId, string refreshToken)
        {
            var newUser = _context.Users.Find(userId);
            newUser.RefreshToken=refreshToken;
            newUser.RefreshTokenEndDate=DateTime.Now.AddDays(_tokenOptions.RefreshTokenExpiration);
        }
    }
}