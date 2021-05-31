using Moq;
using NotSocialNetwork.Application.Entities;
using NotSocialNetwork.Application.Interfaces.Repositories;
using NotSocialNetwork.Application.Interfaces.Systems;
using NotSocialNetwork.Application.Interfaces.UseCases.Publication;
using NotSocialNetwork.Application.Interfaces.UseCases.User;
using NotSocialNetwork.Application.UseCases.Publication;
using System;
using Xunit;

namespace NotSocialNetwork.Core.Tests.UnitTests.Application.UseCases.Publication.Successes
{
    public class AddPublicationSuccessTest
    {
        private readonly PublicationEntity _publication = new PublicationEntity()
        {
            Images = null,
            Author = new UserEntity() { Name = "Name", DateOfBirth = DateTime.Now, Email = "some@gmail.com" },
        };

        [Fact]
        public void Add_AddPublication_Publication()
        {
            // Arrange
            var getablePublication = new Mock<IGetablePublication>();
            var getableUser = new Mock<IGetableUser>();
            var publicationRepository = new Mock<IRepository<PublicationEntity>>();
            var imageRepositorySystem = new Mock<IImageRepositorySystem>();

            var addPublication = new AddPublication(
                                        getablePublication.Object,
                                        getableUser.Object,
                                        publicationRepository.Object,
                                        imageRepositorySystem.Object);

            // Act
            var result = addPublication.Add(_publication);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(_publication, result);
            Assert.Equal(_publication.Id, result.Id);
        }
    }
}
