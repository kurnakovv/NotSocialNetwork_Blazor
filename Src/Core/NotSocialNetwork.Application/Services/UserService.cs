using Microsoft.EntityFrameworkCore;
using NotSocialNetwork.Application.Configs;
using NotSocialNetwork.Application.Entities;
using NotSocialNetwork.Application.Exceptions;
using NotSocialNetwork.Application.Interfaces.Repositories;
using NotSocialNetwork.Application.Interfaces.Services;
using NotSocialNetwork.Application.Interfaces.Systems;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NotSocialNetwork.Application.Services
{
    public class UserService : IUserService
    {
        public UserService(
            IRepository<UserEntity> userRepository,
            IImageRepositorySystem imageRepositorySystem)
        {
            _userRepository = userRepository;
            _imageRepositorySystem = imageRepositorySystem;
        }

        private readonly IRepository<UserEntity> _userRepository;
        private readonly IImageRepositorySystem _imageRepositorySystem;

        public UserEntity Add(UserEntity user)
        {
            if (GetAll().Any(u => u.Email == user.Email))
            {
                throw new ObjectAlreadyExistException($"User by email: {user.Email} already exists.");
            }
            if (GetAll().Any(u => u.Id == user.Id))
            {
                throw new ObjectAlreadyExistException($"User by Id: {user.Id} already exists.");
            }

            var image = _imageRepositorySystem.TrySave(user.Image);
            user.Image = image;

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

        public IEnumerable<UserEntity> GetAll()
        {
            return _userRepository.GetAll().Include(u => u.Image).ToList();
        }

        public UserEntity GetById(Guid id)
        {
            var user = GetAll().FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                throw new ObjectNotFoundException($"User by Id: {id} not found.");
            }

            return user;
        }

        public UserEntity GetByEmail(string email)
        {
            var user = _userRepository.GetAll()
                            .FirstOrDefault(u => u.Email == email);

            if (user == null)
            {
                throw new ObjectNotFoundException($"User by email: {email} not found.");
            }

            return user;
        }

        public IEnumerable<UserEntity> GetByPagination(int index)
        {
            if (index < 0)
            {
                throw new InvalidOperationException("Index cannot be less than 0.");
            }

            var countOfSkipItems = index * PaginationConfig.MAX_ITEMS;

            var users = GetAll()
                            .Skip(countOfSkipItems)
                            .Take(PaginationConfig.MAX_ITEMS);

            if (users.Count() == 0)
            {
                throw new ObjectNotFoundException("No more users.");
            }

            return users;
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
