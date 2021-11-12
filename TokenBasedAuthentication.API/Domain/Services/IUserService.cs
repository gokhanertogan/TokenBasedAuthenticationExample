using TokenBasedAuthentication.API.Domain.Model;
using TokenBasedAuthentication.API.Domain.Responses;

namespace TokenBasedAuthentication.API.Domain.Services
{
    public interface IUserService
    {
        BaseResponse<User> AddUser(User user);

        BaseResponse<User> FindById(int userId);

        BaseResponse<User> FindByEmailAndPassword(string email,string password);

        void SaveRefreshToken(int userId, string refreshToken);

        BaseResponse<User> GetUserWithRefreshToken(string refreshToken);

        void RevokeRefreshToken(User user);
    }
}