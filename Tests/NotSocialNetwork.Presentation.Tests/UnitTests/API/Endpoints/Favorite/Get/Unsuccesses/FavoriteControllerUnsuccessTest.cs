﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NotSocialNetwork.API.Endpoints.Favorite.Get;
using NotSocialNetwork.Application.Interfaces.UseCases.Favorite;
using System;
using Xunit;

namespace NotSocialNetwork.Presentation.Tests.UnitTests.API.Endpoints.Favorite.Get.Unsuccesses
{
    public class FavoriteControllerUnsuccessTest
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
    }
}
