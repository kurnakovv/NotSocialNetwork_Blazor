using System;

namespace NotSocialNetwork.Application.Entities.Abstract
{
    public interface IUser
    {
        string Name { get; }
        string Email { get; }
        DateTimeOffset DateOfBirth { get; }
        ImageEntity Image { get; }
    }
}
