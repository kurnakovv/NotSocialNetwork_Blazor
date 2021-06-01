using NotSocialNetwork.Application.Entities;
using NotSocialNetwork.Application.Exceptions;
using NotSocialNetwork.Application.Interfaces.Repositories;
using NotSocialNetwork.Application.Interfaces.Systems;
using NotSocialNetwork.Application.Interfaces.UseCases.User;
using System.Linq;

namespace NotSocialNetwork.Application.UseCases.User
{
    public class AddUser : IAddableUser
    {
        public AddUser(
            IGetableUser getableUser,
            IRepository<UserEntity> userRepository,
            IImageRepositorySystem imageRepositorySystem)
        {
            _getableUser = getableUser;
            _userRepository = userRepository;
            _imageRepositorySystem = imageRepositorySystem;
        }

        private readonly IGetableUser _getableUser;
        private readonly IRepository<UserEntity> _userRepository;
        private readonly IImageRepositorySystem _imageRepositorySystem;

        public UserEntity Add(UserEntity user)
        {
            if (IsUserAlreadyExist(user))
            {
                throw new ObjectAlreadyExistException($"User by email: {user.Email} already exists.");
            }

            SaveImage(user);

            _userRepository.Add(user);
            _userRepository.Commit();

            return user;
        }

        private bool IsUserAlreadyExist(UserEntity user)
        {
            if(_getableUser.GetAll().Any(u => u.Email == user.Email) ||
                _getableUser.GetAll().Any(u => u.Id == user.Id))
            {
                return true;
            }

            return false;
        }

        private void SaveImage(UserEntity user)
        {
            var image = _imageRepositorySystem.TrySave(user.Image);
            user.Image = image;
        }
    }
}
