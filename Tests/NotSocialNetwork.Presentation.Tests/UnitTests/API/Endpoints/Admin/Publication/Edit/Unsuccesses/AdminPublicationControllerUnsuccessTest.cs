using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NotSocialNetwork.API.Endpoints.Admin.Publication.Edit;
using NotSocialNetwork.Application.DTOs;
using NotSocialNetwork.Application.Exceptions;
using NotSocialNetwork.Application.Interfaces.UseCases.Publication;
using System.Threading.Tasks;
using Xunit;

namespace NotSocialNetwork.Presentation.Tests.UnitTests.API.Endpoints.Admin.Publication.Edit.Unsuccesses
{
    public class AdminPublicationControllerUnsuccessTest
    {
        private static UpdatePublicationDTO _updatePublicationDTO = new UpdatePublicationDTO();

        [Fact]
        public async Task Update_UpdateIfPublicationNotFound_NotFound404()
        {
            // Arrange
            var editPublication = new Mock<IEditablePublicationAsync>();
            var getPublication = new Mock<IGetablePublication>();
            var mapper = new Mock<IMapper>();

            var adminPublicationController = new AdminPublicationController(
                                                editPublication.Object,
                                                getPublication.Object,
                                                mapper.Object);

            getPublication.Setup(gp => gp.GetById(_updatePublicationDTO.Id))
                               .Throws(new ObjectNotFoundException("Publication not found."));

            // Act
            var result = await adminPublicationController.Update(_updatePublicationDTO);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public async Task Delete_DeleteIfPublicationNotFound_NotFound404()
        {
            // Arrange
            var editPublication = new Mock<IEditablePublicationAsync>();
            var getPublication = new Mock<IGetablePublication>();
            var mapper = new Mock<IMapper>();

            var adminPublicationController = new AdminPublicationController(
                                                editPublication.Object,
                                                getPublication.Object,
                                                mapper.Object);

            editPublication.Setup(ep => ep.DeleteAsync(_updatePublicationDTO.Id))
                               .Throws(new ObjectNotFoundException("Publication not found."));

            // Act
            var result = await adminPublicationController.Delete(_updatePublicationDTO.Id);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }
    }
}
