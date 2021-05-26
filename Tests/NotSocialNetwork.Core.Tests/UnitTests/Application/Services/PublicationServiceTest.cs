using Moq;
using NotSocialNetwork.Application.Entities;
using NotSocialNetwork.Application.Exceptions;
using NotSocialNetwork.Application.Interfaces.Repositories;
using NotSocialNetwork.Application.Interfaces.Services;
using NotSocialNetwork.Application.Interfaces.Systems;
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

        private readonly PublicationEntity _publication = new PublicationEntity()
        {
            Images = null,
            Author = new UserEntity() { Name = "Name", DateOfBirth = DateTime.Now, Email = "some@gmail.com" },
        };

        private readonly UserEntity _authorWithoutPublications = new UserEntity()
        {
            Name = "Name",
            DateOfBirth = DateTime.Now,
            Email = "some@gmail.com",
        };

        [Fact]
        public void GetAll_GetAllPublications_Publications()
        {
            // Arrange
            var publicationRepositoryMock = new Mock<IRepository<PublicationEntity>>();
            var publicationService = new PublicationService(publicationRepositoryMock.Object, null, null);

            publicationRepositoryMock.Setup(r => r.GetAll())
                                        .Returns(_publications.AsQueryable());

            // Act
            var result = publicationService.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(_publications, result);
        }

        [Fact]
        public void Add_AddPublication_Publication()
        {
            // Arrange
            var publicationRepositoryMock = new Mock<IRepository<PublicationEntity>>();
            var userService = new Mock<IUserService>();
            var imageRepositorySystem = new Mock<IImageRepositorySystem>();

            var publicationService = new PublicationService(publicationRepositoryMock.Object,
                                                            userService.Object,
                                                            imageRepositorySystem.Object);

            // Act
            var result = publicationService.Add(_publication);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(_publication, result);
            Assert.Equal(_publication.Id, result.Id);
        }

        [Fact]
        public void Add_AddIvnalidPublication_ObjectAlreadyExistException()
        {
            // Arrange
            var publicationRepositoryMock = new Mock<IRepository<PublicationEntity>>();
            var userService = new Mock<IUserService>();
            var imageRepositorySystem = new Mock<IImageRepositorySystem>();


            var publicationService = new PublicationService(publicationRepositoryMock.Object,
                                                            userService.Object, 
                                                            imageRepositorySystem.Object);

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
            var publicationService = new PublicationService(publicationRepositoryMock.Object, 
                                                            null,
                                                            null);

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
            var publicationService = new PublicationService(publicationRepositoryMock.Object,
                                                            null,
                                                            null);

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
            var publicationService = new PublicationService(publicationRepositoryMock.Object,
                                                            null,
                                                            null);

            publicationRepositoryMock.Setup(r => r.GetAll())
                                        .Returns(_publications.AsQueryable());

            // Act
            var result = publicationService.GetById(_publications.ElementAt(0).Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(_publications.ElementAt(0), result);
            Assert.Equal(_publications.ElementAt(0).Id, result.Id);
        }

        [Fact]
        public void Get_GetInvalidPublication_ObjectNotFoundException()
        {
            // Arrange
            var publicationRepositoryMock = new Mock<IRepository<PublicationEntity>>();
            var publicationService = new PublicationService(publicationRepositoryMock.Object,
                                                            null,
                                                            null);

            // Act
            Action act = () => publicationService.GetById(_publication.Id);

            // Assert
            Assert.Throws<ObjectNotFoundException>(act);
        }

        [Fact]
        public void GetAllByAuthorId_GetPublications_Publications()
        {
            // Arrange
            var publicationRepositoryMock = new Mock<IRepository<PublicationEntity>>();
            var userService = new Mock<IUserService>();
            var publicationService = new PublicationService(publicationRepositoryMock.Object,
                                                            userService.Object,
                                                            null);

            publicationRepositoryMock.Setup(r => r.GetAll())
                                        .Returns(_publications.AsQueryable());

            // Act
            var result = publicationService.GetAllByAuthorId(_publications.ElementAt(0).Author.Id);

            // Assert
            Assert.NotNull(result);
            Assert.NotEqual(_publications, result);
            Assert.Equal(_publications.ElementAt(0), result.ElementAt(0));
        }

        [Fact]
        public void GetAllByAuthorId_GetPublicationsByInvalidAuthorId_ObjectNotFoundException()
        {
            // Arrange
            var publicationRepositoryMock = new Mock<IRepository<PublicationEntity>>();
            var userService = new Mock<IUserService>();
            var publicationService = new PublicationService(publicationRepositoryMock.Object,
                                                            userService.Object,
                                                            null);

            // Act
            Action act = () => publicationService.GetAllByAuthorId(_publication.Author.Id);

            // Assert
            Assert.Throws<ObjectNotFoundException>(act);
        }

        [Fact]
        public void GetAllByAuthorId_GetPublicationsIfAuthorHaveNotPublications_ObjectNotFoundException()
        {
            // Arrange
            var publicationRepositoryMock = new Mock<IRepository<PublicationEntity>>();
            var userService = new Mock<IUserService>();
            var publicationService = new PublicationService(publicationRepositoryMock.Object,
                                                            userService.Object,
                                                            null);

            // Act
            Action act = () => publicationService.GetAllByAuthorId(_authorWithoutPublications.Id);

            // Assert
            Assert.Throws<ObjectNotFoundException>(act);
        }

        [Fact]
        public void GetByPagination_GetPubilcations_Pubilcations()
        {
            // Arrange
            var publicationRepositoryMock = new Mock<IRepository<PublicationEntity>>();
            var userService = new Mock<IUserService>();
            var publicationService = new PublicationService(publicationRepositoryMock.Object,
                                                            userService.Object,
                                                            null);

            publicationRepositoryMock.Setup(r => r.GetAll())
                                        .Returns(_publications.AsQueryable());

            // Act
            var result = publicationService.GetByPagination(0);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(_publications, result);
        }

        [Fact]
        public void GetByPagination_GetPublicationsIfIndexLessZero_InvalidOperationException()
        {
            // Arrange
            var publicationRepositoryMock = new Mock<IRepository<PublicationEntity>>();
            var userService = new Mock<IUserService>();
            var publicationService = new PublicationService(publicationRepositoryMock.Object,
                                                            userService.Object,
                                                            null);
            int invalidIndex = -1;

            // Act
            Action act = () => publicationService.GetByPagination(invalidIndex);

            // Assert
            Assert.Throws<InvalidOperationException>(act);
        }

        [Fact]
        public void GetByPagination_GetPublicationsIfPublicationsEnded_ObjectNotFoundException()
        {
            // Arrange
            var publicationRepositoryMock = new Mock<IRepository<PublicationEntity>>();
            var userService = new Mock<IUserService>();
            var publicationService = new PublicationService(publicationRepositoryMock.Object,
                                                            userService.Object,
                                                            null);
            int bigIndex = 10;

            publicationRepositoryMock.Setup(r => r.GetAll())
                                        .Returns(_publications.AsQueryable());

            // Act
            Action act = () => publicationService.GetByPagination(bigIndex);

            // Assert
            Assert.Throws<ObjectNotFoundException>(act);
        }

        [Fact]
        public void Update_UpdatePublication_Publication()
        {
            // Arrange
            var publicationRepositoryMock = new Mock<IRepository<PublicationEntity>>();
            var publicationService = new PublicationService(publicationRepositoryMock.Object,
                                                            null,
                                                            null);

            publicationRepositoryMock.Setup(r => r.GetAll())
                                        .Returns(_publications.AsQueryable());


            // Act
            var result = publicationService.Update(_publications.ElementAt(0));

            // Assert
            Assert.NotNull(result);
            Assert.Equal(_publications.ElementAt(0), result);
            Assert.Equal(_publications.ElementAt(0).Id, result.Id);
        }

        [Fact]
        public void Update_UpdateInvalidPublication_Publication()
        {
            // Arrange
            var publicationRepositoryMock = new Mock<IRepository<PublicationEntity>>();
            var publicationService = new PublicationService(publicationRepositoryMock.Object,
                                                            null,
                                                            null);

            // Act
            Action act = () => publicationService.Update(_publication);

            // Assert
            Assert.Throws<ObjectNotFoundException>(act);
        }
    }
}
