using Moq;
using NotSocialNetwork.Application.Entities;
using NotSocialNetwork.Application.Exceptions;
using NotSocialNetwork.Application.Interfaces.Repositories;
using NotSocialNetwork.Application.Interfaces.Systems;
using NotSocialNetwork.Application.Interfaces.UseCases.Publication;
using NotSocialNetwork.Application.Interfaces.UseCases.User;
using NotSocialNetwork.Application.UseCases.Publication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace NotSocialNetwork.Core.Tests.UnitTests.Application.UseCases.Publication.Unsuccesses
{
    public class AddPublicationUnsuccessTest
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

        [Fact]
        public async Task AddAsync_AddInvalidPublication_ObjectAlreadyExistException()
        {
            // Arrange
            var getablePublication = new Mock<IGetablePublication>();
            var getableUser = new Mock<IGetableUser>();
            var publicationRepository = new Mock<IRepositoryAsync<PublicationEntity>>();
            var imageRepositorySystem = new Mock<IImageRepositorySystemAsync>();

            getablePublication.Setup(gp => gp.GetAll())
                                  .Returns(_publications);

            var addPublication = new AddPublication(
                                        getablePublication.Object,
                                        getableUser.Object,
                                        publicationRepository.Object,
                                        imageRepositorySystem.Object);


            Func<Task> act = async () => await addPublication.AddAsync(_publications.ElementAt(0));

            // Assert
            await Assert.ThrowsAsync<ObjectAlreadyExistException>(act);
        }
    }
}
