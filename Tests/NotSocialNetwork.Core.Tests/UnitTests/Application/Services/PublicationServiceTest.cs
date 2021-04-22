using Moq;
using NotSocialNetwork.Application.Entities;
using NotSocialNetwork.Application.Exceptions;
using NotSocialNetwork.Application.Interfaces.Repositories;
using NotSocialNetwork.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace NotSocialNetwork.Core.Tests.UnitTests.Application.Services
{
    public class PublicationServiceTest
    {
        private readonly IEnumerable<PublicationEntity> _publications = new List<PublicationEntity>()
        {
            new PublicationEntity()
            {
                Title = "Title1",
                Author = new UserEntity()
                {
                    Name = "Name1",
                    DateOfBirth = DateTime.Now,
                    Email = "firstEmail@gmail.com",
                },
                PublicationImages = null,
            },
            new PublicationEntity()
            {
                Title = "Title2",
                Author = new UserEntity()
                {
                    Name = "Name1",
                    DateOfBirth = DateTime.Now,
                    Email = "firstEmail@gmail.com",
                },
                PublicationImages = null,
            }
        };

        private readonly PublicationEntity _publication = new PublicationEntity()
        {
            Title = "Title",
            PublicationImages = null,
            Author = new UserEntity() { Name = "Name", DateOfBirth = DateTime.Now, Email = "some@gmail.com" },
        };

        [Fact]
        public void GetAll_GetAllPublications_Publications()
        {
            // Arrange
            var publicationRepositoryMock = new Mock<IRepository<PublicationEntity>>();
            var publicationService = new PublicationService(publicationRepositoryMock.Object);
            
            publicationRepositoryMock.Setup(r => r.GetAll())
                                        .Returns(_publications.AsQueryable());

            // Act
            var result = publicationService.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(_publications, result);
            Assert.Equal("Title1", result.ElementAt(0).Title);
            Assert.Equal("Title2", result.ElementAt(1).Title);
        }

        [Fact]
        public void Add_AddPublication_Publication()
        {
            // Arrange
            var publicationRepositoryMock = new Mock<IRepository<PublicationEntity>>();
            var publicationService = new PublicationService(publicationRepositoryMock.Object);

            // Act
            var result = publicationService.Add(_publication);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(_publication, result);
            Assert.Equal(_publication.Id, result.Id);
            Assert.Equal(_publication.Title, result.Title);
        }

        [Fact]
        public void Add_AddIvnalidPublication_ObjectAlreadyExistException()
        {
            // Arrange
            var publicationRepositoryMock = new Mock<IRepository<PublicationEntity>>();
            var publicationService = new PublicationService(publicationRepositoryMock.Object);

            publicationRepositoryMock.Setup(r => r.Add(_publication))
                                        .Throws(new ObjectAlreadyExistException($"Publication by Id: {_publication.Id} already exists."));

            // Act
            Action act = () => publicationService.Add(_publication);

            // Assert
            Assert.Throws<ObjectAlreadyExistException>(act);
        }

        [Fact]
        public void Delete_DeletePublication_Publication()
        {
            // Arrange
            var publicationRepositoryMock = new Mock<IRepository<PublicationEntity>>();
            var publicationService = new PublicationService(publicationRepositoryMock.Object);

            publicationRepositoryMock.Setup(r => r.GetAll())
                                        .Returns(_publications.AsQueryable());

            // Act
            var result = publicationService.Delete(_publications.ElementAt(0).Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(_publications.ElementAt(0), result);
            Assert.Equal(_publications.ElementAt(0).Id, result.Id);
        }

        [Fact]
        public void Delete_DeleteInvalidPublication_ObjectNotFoundException()
        {
            // Arrange
            var publicationRepositoryMock = new Mock<IRepository<PublicationEntity>>();
            var publicationService = new PublicationService(publicationRepositoryMock.Object);

            // Act
            Action act = () => publicationService.Delete(_publication.Id);

            // Assert
            Assert.Throws<ObjectNotFoundException>(act);
        }

        [Fact]
        public void Get_GetPublication_Publication()
        {
            // Arrange
            var publicationRepositoryMock = new Mock<IRepository<PublicationEntity>>();
            var publicationService = new PublicationService(publicationRepositoryMock.Object);

            publicationRepositoryMock.Setup(r => r.GetAll())
                                        .Returns(_publications.AsQueryable());

            // Act
            var result = publicationService.GetById(_publications.ElementAt(0).Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(_publications.ElementAt(0), result);
            Assert.Equal(_publications.ElementAt(0).Id, result.Id);
            Assert.Equal(_publications.ElementAt(0).Title, result.Title);
        }

        [Fact]
        public void Get_GetInvalidPublication_ObjectNotFoundException()
        {
            // Arrange
            var publicationRepositoryMock = new Mock<IRepository<PublicationEntity>>();
            var publicationService = new PublicationService(publicationRepositoryMock.Object);

            // Act
            Action act = () => publicationService.GetById(_publication.Id);

            // Assert
            Assert.Throws<ObjectNotFoundException>(act);
        }

        [Fact]
        public void Update_UpdatePublication_Publication()
        {
            // Arrange
            var publicationRepositoryMock = new Mock<IRepository<PublicationEntity>>();
            var publicationService = new PublicationService(publicationRepositoryMock.Object);

            publicationRepositoryMock.Setup(r => r.GetAll())
                                        .Returns(_publications.AsQueryable());

            _publications.ElementAt(0).Title = "TestTitle";

            // Act
            var result = publicationService.Update(_publications.ElementAt(0));

            // Assert
            Assert.NotNull(result);
            Assert.Equal(_publications.ElementAt(0), result);
            Assert.Equal(_publications.ElementAt(0).Id, result.Id);
            Assert.Equal(_publications.ElementAt(0).Title, result.Title);
        }

        [Fact]
        public void Update_UpdateInvalidPublication_Publication()
        {
            // Arrange
            var publicationRepositoryMock = new Mock<IRepository<PublicationEntity>>();
            var publicationService = new PublicationService(publicationRepositoryMock.Object);

            // Act
            Action act = () => publicationService.Update(_publication);

            // Assert
            Assert.Throws<ObjectNotFoundException>(act);
        }
    }
}
