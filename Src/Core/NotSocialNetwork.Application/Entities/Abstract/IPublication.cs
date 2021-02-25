using System.Collections.Generic;

namespace NotSocialNetwork.Application.Entities.Abstract
{
    public interface IPublication
    {
        string Title { get; }
        ICollection<string> Images { get; }
        IUser Author { get; }
    }
}
