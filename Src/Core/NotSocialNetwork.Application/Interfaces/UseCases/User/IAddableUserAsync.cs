using NotSocialNetwork.Application.Entities;
using System.Threading.Tasks;

namespace NotSocialNetwork.Application.Interfaces.UseCases.User
{
    public interface IAddableUserAsync
    {
        Task<UserEntity> AddAsync(UserEntity user);
    }
}
