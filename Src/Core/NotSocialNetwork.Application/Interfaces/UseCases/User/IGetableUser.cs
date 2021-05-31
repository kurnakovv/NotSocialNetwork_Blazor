using NotSocialNetwork.Application.Entities;
using System;
using System.Collections.Generic;

namespace NotSocialNetwork.Application.Interfaces.UseCases.User
{
    public interface IGetableUser
    {
        IEnumerable<UserEntity> GetAll();
        UserEntity GetById(Guid id);
        UserEntity GetByEmail(string email);
        IEnumerable<UserEntity> GetByPagination(int index);
    }
}
