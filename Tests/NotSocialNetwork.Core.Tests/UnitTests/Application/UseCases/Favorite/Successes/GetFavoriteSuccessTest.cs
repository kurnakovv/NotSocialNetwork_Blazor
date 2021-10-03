using Moq;
using NotSocialNetwork.Application.DTOs.Favorite;
using NotSocialNetwork.Application.Entities;
using NotSocialNetwork.Application.Interfaces.UseCases.Publication;
using NotSocialNetwork.Application.Interfaces.UseCases.User;
using NotSocialNetwork.Application.UseCases.Favorite;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace NotSocialNetwork.Core.Tests.UnitTests.Application.UseCases.Favorite.Successes
{
    public class GetFavoriteSuccessTest
    {
        private static UserEntity _author = new UserEntity();
        private static List<PublicationEntity> _publicationsWithAuthor = new List<PublicationEntity>()
        {
            new PublicationEntity()
            {
                Author = _author,
                Text = "SomeText1",
            },
            new PublicationEntity()
            {
                Author = _author,
                Text = "SomeText2",
            },
            new PublicationEntity()
            {
                Author = _author,
                Text = "SomeText3",
            },
        };
        private static List<PublicationEntity> _allPublications = new List<PublicationEntity>()
        {
            new PublicationEntity()
            {
                Author = new UserEntity(),
                Text = "Some text",
            },
            new PublicationEntity()
            {
                Author = new UserEntity(),
                Text = "Some text",
            },
            new PublicationEntity()
            {
                Author = new UserEntity(),
                Text = "Some text",
            },
        };

        private static FavoriteDTO _favoriteDTO = new FavoriteDTO() 
        {
            PublicationId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
        };


        [Fact]
        public void GetPublicationsWithFavorites_GetPublicationsByAuthorId_Publications()
        {
            // Arrange
            _allPublications.AddRange(_publicationsWithAuthor);
            foreach (var publication in _publicationsWithAuthor)
                _author.Favorites.Add(publication);

            var getablePublication = new Mock<IGetablePublication>();
            var getableUser = new Mock<IGetableUser>();

            var getFavorite = new GetFavorite(
                                getablePublication.Object,
                                getableUser.Object);

            getableUser.Setup(u => u.GetById(_author.Id))
                           .Returns(_author);

            getablePublication.Setup(p => p.GetAllByAuthorId(_author.Id))
                                  .Returns(_publicationsWithAuthor);

            // Act
            var result = getFavorite.GetPublicationsWithFavorites(_author.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(_publicationsWithAuthor, result);
            Assert.NotEqual(_allPublications, result);
            Assert.NotEqual(4, result.Count());
        }

        [Fact]
        public void GetAuthorCount_GetAuthorCount_AuthorCount()
        {
            // Arrange
            _allPublications.AddRange(_publicationsWithAuthor);
            _publicationsWithAuthor.FirstOrDefault().Favorites = new List<UserEntity>();
            _publicationsWithAuthor.FirstOrDefault().Favorites.Add(_author);
            var validPublicationCount = 1;
            var inValidPublicationCount = 0;

            var getablePublication = new Mock<IGetablePublication>();
            var getableUser = new Mock<IGetableUser>();

            var getFavorite = new GetFavorite(
                                getablePublication.Object,
                                getableUser.Object);

            getablePublication.Setup(p => p.GetById(_publicationsWithAuthor.FirstOrDefault().Id))
                                  .Returns(_publicationsWithAuthor.FirstOrDefault());

            getableUser.Setup(p => p.GetAll())
                                        .Returns(new List<UserEntity>() { _author, new UserEntity() });

            // Act
            var result = getFavorite.GetAuthorCount(_publicationsWithAuthor.FirstOrDefault().Id);

            // Assert
            Assert.NotEqual(inValidPublicationCount, result);
            Assert.Equal(validPublicationCount, result);
        }

        [Fact]
        public void GetAuthors_GetAuthorsByPublicationId_Authors()
        {
            // Arrange
            _allPublications.AddRange(_publicationsWithAuthor);
            _publicationsWithAuthor.FirstOrDefault().Favorites.Add(_author);

            var getablePublication = new Mock<IGetablePublication>();
            var getableUser = new Mock<IGetableUser>();

            var getFavorite = new GetFavorite(
                                getablePublication.Object,
                                getableUser.Object);

            getablePublication.Setup(p => p.GetById(_publicationsWithAuthor.FirstOrDefault().Id))
                                  .Returns(_publicationsWithAuthor.FirstOrDefault());

            getableUser.Setup(p => p.GetAll())
                                        .Returns(new List<UserEntity>() { _author, new UserEntity() });

            // Act
            var result = getFavorite.GetAuthors(_publicationsWithAuthor.FirstOrDefault().Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(_author, result.FirstOrDefault());
        }

        [Fact]
        public void GetIsFavorite_GetIsFavoriteIfPublicationContainFavoriteByAuthorId_True()
        {
            // Arrange
            _publicationsWithAuthor.FirstOrDefault().Favorites.Add(_author);

            var getablePublication = new Mock<IGetablePublication>();
            var getableUser = new Mock<IGetableUser>();

            var getFavorite = new GetFavorite(
                                getablePublication.Object,
                                getableUser.Object);

            getablePublication.Setup(gp => gp.GetById(_publicationsWithAuthor.FirstOrDefault().Id))
                                  .Returns(_publicationsWithAuthor.FirstOrDefault());

            var favoriteDTO = new FavoriteDTO()
            {
                PublicationId = _publicationsWithAuthor.FirstOrDefault().Id,
                UserId = _author.Id,
            };

            // Act
            var result = getFavorite.GetIsFavorite(favoriteDTO);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void GetIsFavorite_GetIsFavoriteIfPublicationHaveNotFavoriteByAuthorId_False()
        {
            // Arrange
            var getablePublication = new Mock<IGetablePublication>();
            var getableUser = new Mock<IGetableUser>();

            var getFavorite = new GetFavorite(
                                getablePublication.Object,
                                getableUser.Object);

            getablePublication.Setup(gp => gp.GetById(_publicationsWithAuthor.FirstOrDefault().Id))
                                  .Returns(_publicationsWithAuthor.FirstOrDefault());

            var favoriteDTO = new FavoriteDTO()
            {
                PublicationId = _publicationsWithAuthor.FirstOrDefault().Id,
                UserId = Guid.NewGuid(),
            };

            // Act
            var result = getFavorite.GetIsFavorite(favoriteDTO);

            // Assert
            Assert.False(result);
        }
    }
}
