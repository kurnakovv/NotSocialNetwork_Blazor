using Moq;
using NotSocialNetwork.Application.Entities;
using NotSocialNetwork.Application.Exceptions;
using NotSocialNetwork.Application.Interfaces.Repositories;
using NotSocialNetwork.Application.Interfaces.UseCases.User;
using NotSocialNetwork.Application.UseCases.Publication;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace NotSocialNetwork.Core.Tests.UnitTests.Application.UseCases.Publication.Unsuccesses
{
    public class GetPublicationUnsuccessTest
    {
        private readonly IEnumerable<PublicationEntity> _publications = new List<PublicationEntity>()
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

        private readonly UserEntity _authorWithoutPublications = new UserEntity()
        {
            Name = "Name",
            DateOfBirth = DateTime.Now,
            Email = "some@gmail.com",
        };

        [Fact]
        public void Get_GetInvalidPublication_ObjectNotFoundException()
        {
            // Arrange
            var publicationRepository = new Mock<IRepository<PublicationEntity>>();
            var getableUser = new Mock<IGetableUser>();

            var getPublication = new GetPublication(
                                        publicationRepository.Object,
                                        getableUser.Object);

            // Act
            Action act = () => getPublication.GetById(_publications.ElementAt(0).Id);

            // Assert
            Assert.Throws<ObjectNotFoundException>(act);
        }

        [Fact]
        public void GetAllByAuthorId_GetPublicationsByInvalidAuthorId_ObjectNotFoundException()
        {
            // Arrange
            var publicationRepository = new Mock<IRepository<PublicationEntity>>();
            var getableUser = new Mock<IGetableUser>();

            var getPublication = new GetPublication(
                                        publicationRepository.Object,
                                        getableUser.Object);

            // Act
            Action act = () => getPublication.GetAllByAuthorId(_publications.ElementAt(0).Id);

            // Assert
            Assert.Throws<ObjectNotFoundException>(act);
        }

        [Fact]
        public void GetAllByAuthorId_GetPublicationsIfAuthorHaveNotPublications_ObjectNotFoundException()
        {
            // Arrange
            var publicationRepository = new Mock<IRepository<PublicationEntity>>();
            var getableUser = new Mock<IGetableUser>();

            var getPublication = new GetPublication(
                                        publicationRepository.Object,
                                        getableUser.Object);

            publicationRepository.Setup(r => r.GetAll())
                                     .Returns(_publications.AsQueryable());

            // Act
            Action act = () => getPublication.GetAllByAuthorId(_authorWithoutPublications.Id);

            // Assert
            Assert.Throws<ObjectNotFoundException>(act);
        }

        [Fact]
        public void GetByPagination_GetPublicationsIfIndexLessZero_InvalidOperationException()
        {
            // Arrange
            var publicationRepository = new Mock<IRepository<PublicationEntity>>();
            var getableUser = new Mock<IGetableUser>();

            var getPublication = new GetPublication(
                                        publicationRepository.Object,
                                        getableUser.Object);

            publicationRepository.Setup(r => r.GetAll())
                                     .Returns(_publications.AsQueryable());

            int invalidIndex = -1;

            // Act
            Action act = () => getPublication.GetByPagination(invalidIndex);

            // Assert
            Assert.Throws<InvalidOperationException>(act);
        }

        [Fact]
        public void GetByPagination_GetPublicationsIfPublicationsEnded_ObjectNotFoundException()
        {
            // Arrange
            var publicationRepository = new Mock<IRepository<PublicationEntity>>();
            var getableUser = new Mock<IGetableUser>();

            var getPublication = new GetPublication(
                                        publicationRepository.Object,
                                        getableUser.Object);

            publicationRepository.Setup(r => r.GetAll())
                                     .Returns(_publications.AsQueryable());

            int bigIndex = 10;

            // Act
            Action act = () => getPublication.GetByPagination(bigIndex);

            // Assert
            Assert.Throws<ObjectNotFoundException>(act);
        }
    }
}
