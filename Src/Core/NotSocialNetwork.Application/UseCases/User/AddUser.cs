using NotSocialNetwork.Application.Configs;
using NotSocialNetwork.Application.Entities;
using NotSocialNetwork.Application.Exceptions;
using NotSocialNetwork.Application.Interfaces.Repositories;
using NotSocialNetwork.Application.Interfaces.Systems;
using NotSocialNetwork.Application.Interfaces.UseCases.User;
using System.Linq;
using System.Threading.Tasks;

namespace NotSocialNetwork.Application.UseCases.User
{
    public class AddUser : IAddableUserAsync
    {
        public AddUser(
            IGetableUser getableUser,
            IRepositoryAsync<UserEntity> userRepository,
            IImageRepositorySystemAsync imageRepositorySystem)
        {
            _getableUser = getableUser;
            _userRepository = userRepository;
            _imageRepositorySystem = imageRepositorySystem;
        }

        private readonly IGetableUser _getableUser;
        private readonly IRepositoryAsync<UserEntity> _userRepository;
        private readonly IImageRepositorySystemAsync _imageRepositorySystem;

        public async Task<UserEntity> AddAsync(UserEntity user)
        {
            if (IsUserAlreadyExist(user))
            {
                throw new ObjectAlreadyExistException($"User by email: {user.Email} already exists.");
            }

            user.Role = RoleConfig.DEFAULT_USER;

            await SaveImage(user);

            await _userRepository.AddAsync(user);

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

        private async Task SaveImage(UserEntity user)
        {
            var image = await _imageRepositorySystem.TrySaveAsync(user.Image);
            user.Image = image;
        }
    }
}
