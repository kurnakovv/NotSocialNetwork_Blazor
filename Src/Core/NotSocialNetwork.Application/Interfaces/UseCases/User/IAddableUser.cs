using NotSocialNetwork.Application.Entities;

namespace NotSocialNetwork.Application.Interfaces.UseCases.User
{
    public interface IAddableUser
    {
        UserEntity Add(UserEntity user);
    }
}
