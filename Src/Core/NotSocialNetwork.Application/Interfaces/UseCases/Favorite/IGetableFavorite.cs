using NotSocialNetwork.Application.Entities;
using System;
using System.Collections.Generic;

namespace NotSocialNetwork.Application.Interfaces.UseCases.Favorite
{
    public interface IGetableFavorite
    {
        IEnumerable<PublicationEntity> GetPublicationsWithFavorites(Guid authorId);
        IEnumerable<UserEntity> GetAuthors(Guid publicationId);
        int GetAuthorCount(Guid publicationId);
    }
}
