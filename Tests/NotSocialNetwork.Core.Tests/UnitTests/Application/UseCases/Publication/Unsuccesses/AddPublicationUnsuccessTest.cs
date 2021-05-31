using Moq;
using NotSocialNetwork.Application.Entities;
using NotSocialNetwork.Application.Exceptions;
using NotSocialNetwork.Application.Interfaces.Repositories;
using NotSocialNetwork.Application.Interfaces.Systems;
using NotSocialNetwork.Application.Interfaces.UseCases.Publication;
using NotSocialNetwork.Application.Interfaces.UseCases.User;
using NotSocialNetwork.Application.UseCases.Publication;
using System;
using Xunit;

namespace NotSocialNetwork.Core.Tests.UnitTests.Application.UseCases.Publication.Unsuccesses
{
    public class AddPublicationUnsuccessTest
    {
        private readonly PublicationEntity _publication = new PublicationEntity()
        {
            Images = null,
            Author = new UserEntity() { Name = "Name", DateOfBirth = DateTime.Now, Email = "some@gmail.com" },
        };

        [Fact]
        public void Add_AddIvnalidPublication_ObjectAlreadyExistException()
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

            publicationRepository.Setup(r => r.Add(_publication))
                                        .Throws(new ObjectAlreadyExistException($"Publication by Id: {_publication.Id} already exists."));

            // Act
            Action act = () => addPublication.Add(_publication);

            // Assert
            Assert.Throws<ObjectAlreadyExistException>(act);
        }
    }
}
