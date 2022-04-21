using Microsoft.AspNetCore.Mvc;
using Moq;
using NotSocialNetwork.API.Endpoints.Favorite.LikeOrUnlike;
using NotSocialNetwork.Application.DTOs.Favorite;
using NotSocialNetwork.Application.Exceptions;
using NotSocialNetwork.Application.Interfaces.UseCases.Favorite;
using Xunit;

namespace NotSocialNetwork.Presentation.Tests.UnitTests.API.Endpoints.Favorite.LikeOrUnlike.Unsuccesses
{
    public class FavoriteControllerUnsuccessTest
    {
        private static FavoriteDTO _invalidFavoriteDTO = new FavoriteDTO();

        [Fact]
        public void LikeOrUnlike_LikeOrUnlikeIfFavoriteDTONotFound_NotFound404()
        {
            // Arrange
            var likeableFavorite = new Mock<ILikeableFavorite>();

            var favoriteController = new FavoriteController(
                                            likeableFavorite.Object);

            likeableFavorite.Setup(lf => lf.LikeOrUnlike(_invalidFavoriteDTO)).Throws(new ObjectNotFoundException("Objects not found"));

            // Act
            var result = favoriteController.LikeOrUnlike(_invalidFavoriteDTO);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }
    }
}
