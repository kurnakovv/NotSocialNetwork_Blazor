using Microsoft.AspNetCore.Mvc;
using Moq;
using NotSocialNetwork.API.Endpoints.Favorite.LikeOrUnlike;
using NotSocialNetwork.Application.DTOs.Favorite;
using NotSocialNetwork.Application.Interfaces.UseCases.Favorite;
using Xunit;

namespace NotSocialNetwork.Presentation.Tests.UnitTests.API.Endpoints.Favorite.LikeOrUnlike.Successes
{
    public class FavoriteControllerSuccessTest
    {
        private static FavoriteDTO _favoriteDTO = new FavoriteDTO();
        
        
        [Fact]
        public void LikeOrUnlike_LikeOrUnlikeIfPublicationAndAuthoFound_OkObjectResult()
        {
            // Arrange
            var likeableFavorite = new Mock<ILikeableFavorite>();

            var favoriteController = new FavoriteController(
                                            likeableFavorite.Object);

            // Act
            var result = favoriteController.LikeOrUnlike(_favoriteDTO);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result.Result);
        }
    }
}
