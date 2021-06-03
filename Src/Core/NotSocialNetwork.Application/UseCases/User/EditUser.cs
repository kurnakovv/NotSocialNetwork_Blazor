using NotSocialNetwork.Application.Entities;
using NotSocialNetwork.Application.Interfaces.Repositories;
using NotSocialNetwork.Application.Interfaces.UseCases.User;
using System;
using System.Threading.Tasks;

namespace NotSocialNetwork.Application.UseCases.User
{
    public class EditUser : IEditableUserAsync
    {
        public EditUser(
            IGetableUser getableUser,
            IRepositoryAsync<UserEntity> userRepository)
        {
            _getableUser = getableUser;
            _userRepository = userRepository;
        }

        private readonly IGetableUser _getableUser;
        private readonly IRepositoryAsync<UserEntity> _userRepository;

        public async Task<UserEntity> UpdateAsync(UserEntity user)
        {
            _getableUser.GetById(user.Id);

            await _userRepository.UpdateAsync(user);

            return user;
        }

        public async Task<UserEntity> DeleteAsync(Guid id)
        {
            var user = _getableUser.GetById(id);

            await _userRepository.DeleteAsync(id);

            return user;
        }
    }
}
