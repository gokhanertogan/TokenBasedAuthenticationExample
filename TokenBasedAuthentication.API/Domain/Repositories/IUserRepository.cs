using TokenBasedAuthentication.API.Domain.Model;

namespace TokenBasedAuthentication.API.Domain.Repositories
{
    public interface IUserRepository
    {
        void AddUser(User user);

        User FindById(int userId);

        User FindByEmailAndPassword(string email,string password);

        void SaveRefreshToken(int userId,string refreshToken);

        User GetUserWithRefreshToken(string refreshToken);

        void RevokeRefreshToken(User user);
    }
}