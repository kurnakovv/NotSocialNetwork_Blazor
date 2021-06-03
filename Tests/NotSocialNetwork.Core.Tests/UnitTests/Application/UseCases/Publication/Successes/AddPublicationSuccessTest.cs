using Moq;
using NotSocialNetwork.Application.Entities;
using NotSocialNetwork.Application.Interfaces.Repositories;
using NotSocialNetwork.Application.Interfaces.Systems;
using NotSocialNetwork.Application.Interfaces.UseCases.Publication;
using NotSocialNetwork.Application.Interfaces.UseCases.User;
using NotSocialNetwork.Application.UseCases.Publication;
using System;
using System.Threading.Tasks;
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
        public async Task AddAsync_AddPublication_Publication()
        {
            // Arrange
            var getablePublication = new Mock<IGetablePublication>();
            var getableUser = new Mock<IGetableUser>();
            var publicationRepository = new Mock<IRepositoryAsync<PublicationEntity>>();
            var imageRepositorySystem = new Mock<IImageRepositorySystemAsync>();

            var addPublication = new AddPublication(
                                        getablePublication.Object,
                                        getableUser.Object,
                                        publicationRepository.Object,
                                        imageRepositorySystem.Object);

            // Act
            var result = await addPublication.AddAsync(_publication);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(_publication, result);
            Assert.Equal(_publication.Id, result.Id);
        }
    }
}
