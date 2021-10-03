using Moq;
using NotSocialNetwork.Application.DTOs.Favorite;
using NotSocialNetwork.Application.Entities;
using NotSocialNetwork.Application.Exceptions;
using NotSocialNetwork.Application.Interfaces.Repositories;
using NotSocialNetwork.Application.Interfaces.UseCases.Favorite;
using NotSocialNetwork.Application.Interfaces.UseCases.Publication;
using NotSocialNetwork.Application.Interfaces.UseCases.User;
using NotSocialNetwork.Application.UseCases.Favorite;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace NotSocialNetwork.Core.Tests.UnitTests.Application.UseCases.Favorite.Successes
{
    public class LikeFavoriteSuccessTest
    {
        private static UserEntity _author = new UserEntity();
        private static List<PublicationEntity> _publications = new List<PublicationEntity>()
        {
            new PublicationEntity(){ Author = _author },
            new PublicationEntity(){ Author = _author },
            new PublicationEntity(){ Author = _author },
        };

        private static FavoriteDTO _favorite = new FavoriteDTO()
        {
            PublicationId = _publications.FirstOrDefault().Id,
            UserId = _author.Id,
        };

        [Fact]
        public void LikeOrUnlike_LikeIfNotHaveFavorite_FavoriteEntity()
        {
            // Arrange
            var getUser = new Mock<IGetableUser>();
            var getPublication = new Mock<IGetablePublication>();
            var getFavorite = new Mock<IGetableFavorite>();
            var repository = new Mock<IRepositoryAsync<PublicationEntity>>();

            var likeFavorite = new LikeFavorite(
                                    getUser.Object,
                                    getPublication.Object,
                                    getFavorite.Object,
                                    repository.Object);

            getUser.Setup(gu => gu.GetById(_author.Id))
                       .Returns(_author);

            getPublication.Setup(gp => gp.GetById(_publications.FirstOrDefault().Id))
                              .Returns(_publications.FirstOrDefault());

            getFavorite.Setup(gf => gf.GetPublicationsWithFavorites(_author.Id))
                .Throws(new FavoritesNotFoundException("Favorites not found"));

            getFavorite.Setup(gf => gf.GetAuthors(_publications.FirstOrDefault().Id))
               .Throws(new FavoritesNotFoundException("Favorites not found"));

            // Act
            var result = likeFavorite.LikeOrUnlike(_favorite);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsLike);
        }

        [Fact]
        public void LikeOrUnlike_UnlikeIfHaveFavorite_FavoriteEntity()
        {
            // Arrange
            var getUser = new Mock<IGetableUser>();
            var getPublication = new Mock<IGetablePublication>();
            var getFavorite = new Mock<IGetableFavorite>();
            var repository = new Mock<IRepositoryAsync<PublicationEntity>>();

            var likeFavorite = new LikeFavorite(
                                    getUser.Object,
                                    getPublication.Object,
                                    getFavorite.Object,
                                    repository.Object);

            getUser.Setup(gu => gu.GetById(_author.Id))
                       .Returns(_author);

            getPublication.Setup(gp => gp.GetById(_publications.FirstOrDefault().Id))
                              .Returns(_publications.FirstOrDefault());

            getFavorite.Setup(gf => gf.GetPublicationsWithFavorites(_author.Id))
                .Returns(_publications);

            // Act
            var result = likeFavorite.LikeOrUnlike(_favorite);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.IsLike);
        }
    }
}
