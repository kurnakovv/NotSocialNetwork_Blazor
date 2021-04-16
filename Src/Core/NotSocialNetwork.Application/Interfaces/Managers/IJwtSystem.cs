using NotSocialNetwork.Application.Entities;

namespace NotSocialNetwork.Application.Interfaces.Managers
{
    public interface IJwtSystem
    {
        string GenerateToken(UserEntity id);
    }
}
