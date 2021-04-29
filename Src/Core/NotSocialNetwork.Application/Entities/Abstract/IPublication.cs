using System.Collections.Generic;

namespace NotSocialNetwork.Application.Entities.Abstract
{
    public interface IPublication
    {
        string Title { get; }
        ICollection<ImageEntity> Images { get; }
        UserEntity Author { get; }
    }
}
