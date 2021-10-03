using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NotSocialNetwork.API.Endpoints.Favorite.Get;
using NotSocialNetwork.Application.Exceptions;
using NotSocialNetwork.Application.Interfaces.UseCases.Favorite;
using System;
using Xunit;

namespace NotSocialNetwork.Presentation.Tests.UnitTests.API.Endpoints.Favorite.Get.Unsuccesses
{
    public class FavoriteControllerUnsuccessTest
    {
        [Fact]
        public void Get_GetPublicationsIfAuthorNotFound_NotFound404()
        {
            // Arrange
            var invalidAuthorId = Guid.NewGuid();
            var getableFavorite = new Mock<IGetableFavorite>();
            var mapper = new Mock<IMapper>();

            var favoriteController = new FavoriteController(
                                            getableFavorite.Object,
                                            mapper.Object);

            getableFavorite.Setup(gf => gf.GetPublicationsWithFavorites(invalidAuthorId))
                                    .Throws(new ObjectNotFoundException("Author not found."));

            // Act
            var result = favoriteController.Get(invalidAuthorId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public void Get_GetPublicationsWithFavorites_NotFound404()
        {
            // Arrange
            var invalidAuthorId = Guid.NewGuid();
            var getableFavorite = new Mock<IGetableFavorite>();
            var mapper = new Mock<IMapper>();

            var favoriteController = new FavoriteController(
                                            getableFavorite.Object,
                                            mapper.Object);

            getableFavorite.Setup(gf => gf.GetPublicationsWithFavorites(invalidAuthorId))
                                    .Throws(new FavoritesNotFoundException("Favorites not found."));

            // Act
            var result = favoriteController.Get(invalidAuthorId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public void GetAuthorCount_GetAuthorCountIfPublictionNotFound_NotFound404()
        {
            // Arrange
            var invalidPublicationId = Guid.NewGuid();
            var getableFavorite = new Mock<IGetableFavorite>();
            var mapper = new Mock<IMapper>();

            var favoriteController = new FavoriteController(
                                            getableFavorite.Object,
                                            mapper.Object);

            getableFavorite.Setup(gf => gf.GetAuthorCount(invalidPublicationId))
                                    .Throws(new ObjectNotFoundException("Publication not found."));

            // Act
            var result = favoriteController.GetAuthorCount(invalidPublicationId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public void GetAuthors_GetAuthorsIfPublicationsNotFound_NotFound404()
        {
            // Arrange
            var invalidPublicationId = Guid.NewGuid();
            var getableFavorite = new Mock<IGetableFavorite>();
            var mapper = new Mock<IMapper>();

            var favoriteController = new FavoriteController(
                                            getableFavorite.Object,
                                            mapper.Object);

            getableFavorite.Setup(gf => gf.GetAuthors(invalidPublicationId))
                                    .Throws(new ObjectNotFoundException("Publication not found."));

            // Act
            var result = favoriteController.GetAuthors(invalidPublicationId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public void GetAuthors_GetAuthorsIfPublicationsHaveNotFavorites_NotFound404()
        {
            // Arrange
            var publicationId = Guid.NewGuid();
            var getableFavorite = new Mock<IGetableFavorite>();
            var mapper = new Mock<IMapper>();

            var favoriteController = new FavoriteController(
                                            getableFavorite.Object,
                                            mapper.Object);

            getableFavorite.Setup(gf => gf.GetAuthors(publicationId))
                                    .Throws(new FavoritesNotFoundException("Publication dont have favorites."));

            // Act
            var result = favoriteController.GetAuthors(publicationId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }
    }
}
