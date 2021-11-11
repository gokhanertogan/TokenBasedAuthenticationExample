using System;
using TokenBasedAuthentication.API.Domain.Model;
using TokenBasedAuthentication.API.Domain.Repositories;
using TokenBasedAuthentication.API.Domain.Responses;
using TokenBasedAuthentication.API.Domain.Services;
using TokenBasedAuthentication.API.Domain.UnitOfWork;

namespace TokenBasedAuthentication.API.Services
{
    public class UserService : IUserService
    {

        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public UserResponse AddUser(User user)
        {
            try
            {
                _userRepository.AddUser(user);
                _unitOfWork.Complete();
                return new UserResponse(user);
            }
            catch (Exception ex)
            {
                return new UserResponse($"Error occurred while adding a new user :{ex.Message}");
            }
        }

        public UserResponse FindByEmailAndPassword(string email, string password)
        {
            try
            {
                var user = _userRepository.FindByEmailAndPassword(email, password);
                if (user == null)
                    return new UserResponse("Could not found user");

                return new UserResponse(user);
            }
            catch (Exception ex)
            {
                return new UserResponse($"Error occurred while finding a new user :{ex.Message}");
            }
        }

        public UserResponse FindById(int userId)
        {
            try
            {
                var user = _userRepository.FindById(userId);
                if (user == null)
                    return new UserResponse("Could not found user");

                return new UserResponse(user);
            }
            catch (Exception ex)
            {
                return new UserResponse($"Error occurred while finding a new user :{ex.Message}");
            }
        }

        public UserResponse GetUserWithRefreshToken(string refreshToken)
        {
            try
            {
                var user = _userRepository.GetUserWithRefreshToken(refreshToken);
                if (user == null)
                    return new UserResponse("Could not found user");

                return new UserResponse(user);
            }
            catch (Exception ex)
            {
                return new UserResponse($"Error occurred while finding a new user :{ex.Message}");
            }
        }

        public void RevokeRefreshToken(User user)
        {
            try
            {
                _userRepository.RevokeRefreshToken(user);
            }
            catch
            {
                throw;
            }
        }

        public void SaveRefreshToken(int userId, string refreshToken)
        {
            try
            {
                _userRepository.SaveRefreshToken(userId, refreshToken);
                _unitOfWork.Complete();
            }
            catch
            {
                throw;
            }
        }
    }
}