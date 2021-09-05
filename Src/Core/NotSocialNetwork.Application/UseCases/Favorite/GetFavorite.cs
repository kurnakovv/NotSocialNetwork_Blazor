using NotSocialNetwork.Application.Entities;
using NotSocialNetwork.Application.Exceptions;
using NotSocialNetwork.Application.Interfaces.UseCases.Favorite;
using NotSocialNetwork.Application.Interfaces.UseCases.Publication;
using NotSocialNetwork.Application.Interfaces.UseCases.User;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NotSocialNetwork.Application.UseCases.Favorite
{
    public class GetFavorite : IGetableFavorite
    {
        public GetFavorite(
            IGetablePublication getablePublication,
            IGetableUser getableUser)
        {
            _getablePublication = getablePublication;
            _getableUser = getableUser;
        }

        private readonly IGetablePublication _getablePublication;
        private readonly IGetableUser _getableUser;

        public IEnumerable<PublicationEntity> GetPublicationsWithFavorites(Guid authorId)
        {
            var author = _getableUser.GetById(authorId);
            CheckAuthorFavorites(author);
            return author.Favorites;
        }

        public int GetAuthorCount(Guid publicationId)
        {
            var publication = _getablePublication.GetById(publicationId);
            CheckPublicationFavorites(publication);
            return publication.Favorites.Count();
        }

        public IEnumerable<UserEntity> GetAuthors(Guid publicationId)
        {
            var publication = _getablePublication.GetById(publicationId);
            CheckPublicationFavorites(publication);
            return publication.Favorites;
        }

        private void CheckAuthorFavorites(UserEntity author)
        {
            if (IsFavoritesInAuthorFound(author) == false)
            {
                throw new FavoritesNotFoundException($"Author by id: {author.Id} doesn't have a favorites.");
            }
        }

        private bool IsFavoritesInAuthorFound(UserEntity author)
        {
            if(author.Favorites != null &&
               author.Favorites.Count != 0)
            {
                return true;
            }

            return false;
        }

        private void CheckPublicationFavorites(PublicationEntity publication)
        {
            if (IsFavoritesInPublicationFound(publication) == false)
            {
                throw new FavoritesNotFoundException($"Publication by id: {publication.Id} doesn't have a favorites.");
            }
        }

        private bool IsFavoritesInPublicationFound(PublicationEntity publication)
        {
            if (publication.Favorites != null &&
               publication.Favorites.Count != 0)
            {
                return true;
            }

            return false;
        }
    }
}
