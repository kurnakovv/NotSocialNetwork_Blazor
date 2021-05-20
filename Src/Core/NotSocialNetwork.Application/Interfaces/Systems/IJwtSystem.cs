using NotSocialNetwork.Application.Entities;

namespace NotSocialNetwork.Application.Interfaces.Systems
{
    public interface IJwtSystem
    {
        string GenerateToken(UserEntity user);
    }
}
