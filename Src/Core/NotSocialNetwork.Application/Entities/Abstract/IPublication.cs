using System.Collections.Generic;

namespace NotSocialNetwork.Application.Entities.Abstract
{
    public interface IPublication
    {
        ICollection<ImageEntity> Images { get; }
        UserEntity Author { get; }
    }
}
