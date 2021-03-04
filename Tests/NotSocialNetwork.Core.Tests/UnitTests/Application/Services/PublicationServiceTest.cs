using Moq;
using NotSocialNetwork.Application.Entities;
using NotSocialNetwork.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
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
            var publicationService = new Mock<IPublicationService>();

            // Act
            var result = publicationService.Setup(p => p.GetAll()).Returns(_publications);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(_publications, publicationService.Object.GetAll());
        }

        [Fact]
        public void Add_AddPublication_Publication()
        {
            // Arrange
            var publicationService = new Mock<IPublicationService>();

            // Act
            var result = publicationService.Setup(p => p.Add(_publication)).Returns(_publication);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(_publication, publicationService.Object.Add(_publication));
        }

        [Fact]
        public void Delete_DeletePublication_Publication()
        {
            // Arrange
            var publicationService = new Mock<IPublicationService>();

            // Act
            var result = publicationService.Setup(p => p.Delete(_publication.Id)).Returns(_publication);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(_publication, publicationService.Object.Delete(_publication.Id));
        }

        [Fact]
        public void Get_GetPublication_Publication()
        {
            // Arrange
            var publicationService = new Mock<IPublicationService>();

            // Act
            var result = publicationService.Setup(p => p.GetById(_publication.Id)).Returns(_publication);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(_publication, publicationService.Object.GetById(_publication.Id));
        }

        [Fact]
        public void Update_UpdatePublication_Publication()
        {
            // Arrange
            var publicationService = new Mock<IPublicationService>();

            // Act
            var result = publicationService.Setup(p => p.Update(_publication)).Returns(_publication);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(_publication, publicationService.Object.Update(_publication));
        }
    }
}
