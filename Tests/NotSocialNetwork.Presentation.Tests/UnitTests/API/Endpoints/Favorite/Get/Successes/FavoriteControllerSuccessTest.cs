using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NotSocialNetwork.API.Endpoints.Favorite.Get;
using NotSocialNetwork.Application.Exceptions;
using NotSocialNetwork.Application.Interfaces.UseCases.Favorite;
using System;
using Xunit;

namespace NotSocialNetwork.Presentation.Tests.UnitTests.API.Endpoints.Favorite.Get.Successes
{
    public class FavoriteControllerSuccessTest
    {
        [Fact]
        public void Get_GetPublicationsWithFavorites_OkObjectResult()
        {
            // Arrange
            var getableFavorite = new Mock<IGetableFavorite>();
            var mapper = new Mock<IMapper>();

            var favoriteController = new FavoriteController(
                                            getableFavorite.Object,
                                            mapper.Object);

            // Act
            var result = favoriteController.Get(Guid.NewGuid());

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GetAuthorCount_GetAuthorCountIfPublicationFound_OkObjectResult()
        {
            // Arrange
            var getableFavorite = new Mock<IGetableFavorite>();
            var mapper = new Mock<IMapper>();

            var favoriteController = new FavoriteController(
                                            getableFavorite.Object,
                                            mapper.Object);

            // Act
            var result = favoriteController.GetAuthorCount(Guid.NewGuid());

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GetAuthorCount_GetAuthorCountIfPublicationDontHaveFavorites_OkObjectResult()
        {
            // Arrange
            var getableFavorite = new Mock<IGetableFavorite>();
            var mapper = new Mock<IMapper>();

            var favoriteController = new FavoriteController(
                                            getableFavorite.Object,
                                            mapper.Object);

            var validPublicationId = Guid.NewGuid();

            getableFavorite.Setup(gf => gf.GetAuthorCount(validPublicationId))
                                .Throws(new FavoritesNotFoundException("Favorites not found"));

            // Act
            var result = favoriteController.GetAuthorCount(validPublicationId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(0, result.Value);
        }

        [Fact]
        public void GetAuthors_GetAuthorIfPublicationHave_OkObjectResult()
        {
            // Arrange
            var getableFavorite = new Mock<IGetableFavorite>();
            var mapper = new Mock<IMapper>();

            var favoriteController = new FavoriteController(
                                            getableFavorite.Object,
                                            mapper.Object);

            // Act
            var result = favoriteController.GetAuthors(Guid.NewGuid());

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result.Result);
        }
    }
}
