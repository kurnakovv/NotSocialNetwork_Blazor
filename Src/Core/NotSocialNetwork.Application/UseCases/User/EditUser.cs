using NotSocialNetwork.Application.Entities;
using NotSocialNetwork.Application.Interfaces.Repositories;
using NotSocialNetwork.Application.Interfaces.UseCases.User;
using System;

namespace NotSocialNetwork.Application.UseCases.User
{
    public class EditUser : IEditableUser
    {
        public EditUser(
            IGetableUser getableUser,
            IRepository<UserEntity> userRepository)
        {
            _getableUser = getableUser;
            _userRepository = userRepository;
        }

        private readonly IGetableUser _getableUser;
        private readonly IRepository<UserEntity> _userRepository;

        public UserEntity Update(UserEntity user)
        {
            _getableUser.GetById(user.Id);

            _userRepository.Update(user);
            _userRepository.Commit();

            return user;
        }

        public UserEntity Delete(Guid id)
        {
            var user = _getableUser.GetById(id);

            _userRepository.Delete(user.Id);
            _userRepository.Commit();

            return user;
        }
    }
}
