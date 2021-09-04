using NotSocialNetwork.Application.Entities;
using System;
using System.Collections.Generic;

namespace NotSocialNetwork.Application.Interfaces.UseCases.Favorite
{
    public interface IGetableFavoriteAsync
    {
        IEnumerable<PublicationEntity> GetPublicationsWithFavoritesAsync(Guid authorId);
        IEnumerable<UserEntity> GetAuthorsAsync(Guid publicationId);
        int GetAuthorCountAsync(Guid publicationId);
    }
}
