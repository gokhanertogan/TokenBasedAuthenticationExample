using TokenBasedAuthentication.API.Domain.Model;
using TokenBasedAuthentication.API.Domain.Responses;

namespace TokenBasedAuthentication.API.Domain.Services
{
    public interface IUserService
    {
        UserResponse AddUser(User user);

        UserResponse FindById(int userId);

        UserResponse FindByEmailAndPassword(string email,string password);

        void SaveRefreshToken(int userId, string refreshToken);

        UserResponse GetUserWithRefreshToken(string refreshToken);

        void RevokeRefreshToken(User user);
    }
}