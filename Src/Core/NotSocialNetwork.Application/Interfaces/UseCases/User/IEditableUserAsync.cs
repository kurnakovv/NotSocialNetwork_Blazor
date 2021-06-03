using NotSocialNetwork.Application.Entities;
using System;
using System.Threading.Tasks;

namespace NotSocialNetwork.Application.Interfaces.UseCases.User
{
    public interface IEditableUserAsync
    {
        Task<UserEntity> UpdateAsync(UserEntity user);
        Task<UserEntity> DeleteAsync(Guid id);
    }
}
