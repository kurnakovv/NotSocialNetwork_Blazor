using NotSocialNetwork.Application.Entities;
using System;

namespace NotSocialNetwork.Application.Interfaces.UseCases.User
{
    public interface IEditableUser
    {
        UserEntity Update(UserEntity user);
        UserEntity Delete(Guid id);
    }
}
