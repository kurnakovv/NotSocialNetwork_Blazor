using System.Collections.Generic;

namespace NotSocialNetwork.Application.Entities.Abstract
{
    public interface IPublication
    {
        string Title { get; }
        ICollection<PublicationImageEntity> PublicationImages { get; }
        UserEntity Author { get; }
    }
}
