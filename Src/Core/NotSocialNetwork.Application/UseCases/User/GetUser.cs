using Microsoft.EntityFrameworkCore;
using NotSocialNetwork.Application.Configs;
using NotSocialNetwork.Application.Entities;
using NotSocialNetwork.Application.Exceptions;
using NotSocialNetwork.Application.Interfaces.Repositories;
using NotSocialNetwork.Application.Interfaces.UseCases.User;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NotSocialNetwork.Application.UseCases.User
{
    public class GetUser : IGetableUser
    {
        public GetUser(
            IRepositoryAsync<UserEntity> userRepository)
        {
            _userRepository = userRepository;
        }

        private readonly IRepositoryAsync<UserEntity> _userRepository;

        public IEnumerable<UserEntity> GetAll()
        {
            return _userRepository.GetAll()
                            .Include(u => u.Image);
        }

        public UserEntity GetByEmail(string email)
        {
            var user = GetAll()
                           .FirstOrDefault(u => u.Email == email);

            CheckUserIsValid(user, $"User by email: {email} not found.");

            return user;
        }

        public UserEntity GetById(Guid id)
        {
            var user = GetAll()
                           .FirstOrDefault(u => u.Id == id);

            CheckUserIsValid(user, $"User by Id: {id} not found.");

            return user;
        }

        public IEnumerable<UserEntity> GetByPagination(int index)
        {
            CheckIndexIsValid(index);
            var users = GetUsersByPagination(index);
            CheckUsersCountIsValid(users);

            return users;
        }

        private void CheckUserIsValid(UserEntity user, string exceptionMessage)
        {
            if (user == null)
            {
                throw new ObjectNotFoundException(exceptionMessage);
            }
        }

        private void CheckIndexIsValid(int index)
        {
            if (IsInvalidIndex(index))
            {
                throw new InvalidOperationException("Index cannot be less than 0.");
            }
        }

        private IEnumerable<UserEntity> GetUsersByPagination(int index)
        {
            var countOfSkipItems = index * PaginationConfig.MAX_ITEMS;
            var users = GetAll()
                            .Skip(countOfSkipItems)
                            .Take(PaginationConfig.MAX_ITEMS);

            return users;
        }

        private void CheckUsersCountIsValid(IEnumerable<UserEntity> users)
        {
            if (IsEmptyUsersCount(users))
            {
                throw new ObjectNotFoundException("No more users.");
            }
        }

        private bool IsInvalidIndex(int index)
        {
            if (index < 0)
            {
                return true;
            }

            return false;
        }

        private bool IsEmptyUsersCount(IEnumerable<UserEntity> users)
        {
            if (users.Count() == 0)
            {
                return true;
            }

            return false;
        }
    }
}
