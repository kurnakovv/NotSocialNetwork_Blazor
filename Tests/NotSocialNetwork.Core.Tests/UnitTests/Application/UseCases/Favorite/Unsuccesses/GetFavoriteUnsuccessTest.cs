using Moq;
using NotSocialNetwork.Application.Entities;
using NotSocialNetwork.Application.Exceptions;
using NotSocialNetwork.Application.Interfaces.UseCases.Publication;
using NotSocialNetwork.Application.Interfaces.UseCases.User;
using NotSocialNetwork.Application.UseCases.Favorite;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace NotSocialNetwork.Core.Tests.UnitTests.Application.UseCases.Favorite.Unsuccesses
{
    public class GetFavoriteUnsuccessTest
    {
        private static UserEntity _author = new UserEntity();
        private static List<PublicationEntity> _publications = new List<PublicationEntity>()
        {
            new PublicationEntity(),
        };

        [Fact]
        public void GetPublicationsWithFavoritesAsync_GetPublicationsByAuthorIdIfAuthorDontHaveFavorites_FavoritesNotFoundException()
        {
            // Arrange
            var getablePublication = new Mock<IGetablePublication>();
            var getableUser = new Mock<IGetableUser>();

            var getFavorite = new GetFavorite(
                                getablePublication.Object,
                                getableUser.Object);

            getableUser.Setup(u => u.GetById(_author.Id))
                          .Returns(_author);

            // Act
            Action act = () => getFavorite.GetPublicationsWithFavoritesAsync(_author.Id);

            // Assert
            Assert.Throws<FavoritesNotFoundException>(act);
        }

        [Fact]
        public void GetAuthorCountAsync_GetPublicationCountIfPublicationDontHaveFavorites_FavoritesNotFoundException()
        {
            // Arrange
            var getablePublication = new Mock<IGetablePublication>();
            var getableUser = new Mock<IGetableUser>();

            var getFavorite = new GetFavorite(
                                getablePublication.Object,
                                getableUser.Object);

            getablePublication.Setup(p => p.GetById(_publications.FirstOrDefault().Id))
                                  .Returns(_publications.FirstOrDefault());

            // Act
            Action act = () => getFavorite.GetAuthorCountAsync(_publications.FirstOrDefault().Id);

            // Assert
            Assert.Throws<FavoritesNotFoundException>(act);
        }

        [Fact]
        public void GetAuthorsAsync_GetAuthorsIfPublicationDontHaveFavorites_FavoritesNotFoundException()
        {
            // Arrange
            var getablePublication = new Mock<IGetablePublication>();
            var getableUser = new Mock<IGetableUser>();

            var getFavorite = new GetFavorite(
                                getablePublication.Object,
                                getableUser.Object);

            getablePublication.Setup(p => p.GetById(_publications.FirstOrDefault().Id))
                                  .Returns(_publications.FirstOrDefault());

            // Act
            Action act = () => getFavorite.GetAuthorsAsync(_publications.FirstOrDefault().Id);

            // Assert
            Assert.Throws<FavoritesNotFoundException>(act);
        }
    }
}
