using NotSocialNetwork.Application.Entities;
using NotSocialNetwork.Application.Exceptions;
using NotSocialNetwork.Application.Interfaces.Repositories;
using NotSocialNetwork.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NotSocialNetwork.Application.Services
{
    public class UserService : IUserService
    {
        public UserService(
            IRepository<UserEntity> userRepository)
        {
            _userRepository = userRepository;
        }

        private readonly IRepository<UserEntity> _userRepository;

        public UserEntity Add(UserEntity user)
        {
            if(GetAll().Any(u => u.Id == user.Id))
            {
                throw new ObjectAlreadyExistException($"User by Id: {user.Id} already exists.");
            }

            _userRepository.Add(user);
            _userRepository.Commit();

            return user;
        }

        public UserEntity Delete(Guid id)
        {
            var user = GetById(id);
            
            _userRepository.Delete(user.Id);
            _userRepository.Commit();

            return user;
        }

        public IEnumerable<UserEntity> GetAll() => _userRepository.GetAll();

        public UserEntity GetById(Guid id)
        {
            var user = _userRepository.Get(id);

            if (user == null)
            {
                throw new ObjectNotFoundException($"User by Id: {id} not found.");
            }

            return user;
        }

        public UserEntity Update(UserEntity user)
        {
            GetById(user.Id);

            _userRepository.Update(user);
            _userRepository.Commit();

            return user;
        }
    }
}
