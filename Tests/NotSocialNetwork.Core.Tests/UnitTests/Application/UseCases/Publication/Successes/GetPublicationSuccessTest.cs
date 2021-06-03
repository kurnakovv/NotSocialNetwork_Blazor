using Moq;
using NotSocialNetwork.Application.Entities;
using NotSocialNetwork.Application.Interfaces.Repositories;
using NotSocialNetwork.Application.Interfaces.UseCases.User;
using NotSocialNetwork.Application.UseCases.Publication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace NotSocialNetwork.Core.Tests.UnitTests.Application.UseCases.Publication.Successes
{
    public class GetPublicationSuccessTest
    {
        private readonly List<PublicationEntity> _publications = new List<PublicationEntity>()
        {
            new PublicationEntity()
            {
                Author = new UserEntity()
                {
                    Name = "Name1",
                    DateOfBirth = DateTime.Now,
                    Email = "firstEmail@gmail.com",
                },
                Images = null,
            },
            new PublicationEntity()
            {
                Author = new UserEntity()
                {
                    Name = "Name1",
                    DateOfBirth = DateTime.Now,
                    Email = "firstEmail@gmail.com",
                },
                Images = null,
            }
        };

        [Fact]
        public void GetAll_GetAllPublications_Publications()
        {
            // Arrange
            var publicationRepository = new Mock<IRepositoryAsync<PublicationEntity>>();
            var getableUser = new Mock<IGetableUser>();

            var getPublication = new GetPublication(
                                        publicationRepository.Object,
                                        getableUser.Object);
            publicationRepository.Setup(r => r.GetAll())
                                        .Returns(_publications.AsQueryable());

            // Act
            var result = getPublication.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(_publications, result);
        }

        [Fact]
        public void GetById_GetPublication_Publication()
        {
            // Arrange
            var publicationRepository = new Mock<IRepositoryAsync<PublicationEntity>>();
            var getableUser = new Mock<IGetableUser>();

            var getPublication = new GetPublication(
                                        publicationRepository.Object,
                                        getableUser.Object);

            publicationRepository.Setup(r => r.GetAll())
                                    .Returns(_publications.AsQueryable());

            // Act
            var result = getPublication.GetById(_publications.ElementAt(0).Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(_publications.ElementAt(0), result);
            Assert.Equal(_publications.ElementAt(0).Id, result.Id);
        }

        [Fact]
        public void GetAllByAuthorId_GetPublications_Publications()
        {
            // Arrange
            var publicationRepository = new Mock<IRepositoryAsync<PublicationEntity>>();
            var getableUser = new Mock<IGetableUser>();

            var getPublication = new GetPublication(
                                        publicationRepository.Object,
                                        getableUser.Object);

            publicationRepository.Setup(r => r.GetAll())
                                    .Returns(_publications.AsQueryable());

            // Act
            var result = getPublication.GetAllByAuthorId(_publications.ElementAt(0).Author.Id);

            // Assert
            Assert.NotNull(result);
            Assert.NotEqual(_publications, result);
            Assert.Equal(_publications.ElementAt(0), result.ElementAt(0));
        }

        [Fact]
        public void GetByPagination_GetPubilcations_Pubilcations()
        {
            // Arrange
            var publicationRepository = new Mock<IRepositoryAsync<PublicationEntity>>();
            var getableUser = new Mock<IGetableUser>();

            var getPublication = new GetPublication(
                                        publicationRepository.Object,
                                        getableUser.Object);

            publicationRepository.Setup(r => r.GetAll())
                                    .Returns(_publications.AsQueryable());

            // Act
            var result = getPublication.GetByPagination(0);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(_publications, result);
        }
    }
}
