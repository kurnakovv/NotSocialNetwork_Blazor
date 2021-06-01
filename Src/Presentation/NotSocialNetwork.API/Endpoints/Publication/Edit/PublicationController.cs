using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotSocialNetwork.Application.DTOs;
using NotSocialNetwork.Application.Exceptions;
using NotSocialNetwork.Application.Interfaces.UseCases.Publication;
using Swashbuckle.AspNetCore.Annotations;
using System;

namespace NotSocialNetwork.API.Endpoints.Publication.Edit
{
    [Route("api/publication")]
    [ApiController]
    [Authorize]
    public class PublicationController : ControllerBase
    {
        public PublicationController(
            IEditablePublication editablePublication,
            IGetablePublication getablePublication,
            IMapper mapper)
        {
            _editablePublication = editablePublication;
            _getablePublication = getablePublication;
            _mapper = mapper;
        }

        private readonly IEditablePublication _editablePublication;
        private readonly IGetablePublication _getablePublication;
        private readonly IMapper _mapper;

        /// <summary>
        /// Update publication.
        /// </summary>
        /// <param name="publication">Publication parameters.</param>
        /// <returns>Publication.</returns>
        [HttpPut]
        [ProducesResponseType(typeof(PublicationDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [SwaggerOperation(
            Summary = "Update.",
            Description = "Update publication."
        )]
        public ActionResult<PublicationDTO> Update(UpdatePublicationDTO publication)
        {
            try
            {
                // TODO: Add automapper for update.

                //var publicationEntity =
                //    _mapper.Map<PublicationEntity>(publication);

                var publicationEntity = _getablePublication.GetById(publication.Id);
                publicationEntity.Text = publication.Text;



                _editablePublication.Update(publicationEntity);

                return Ok(publication);
            }
            catch (ObjectNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Delete publication by id.
        /// </summary>
        /// <param name="id">Publication id.</param>
        /// <returns>Publication.</returns>
        [HttpDelete]
        [ProducesResponseType(typeof(PublicationDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [SwaggerOperation(
            Summary = "Delete by id.",
            Description = "Delete publication by id."
        )]
        public ActionResult<PublicationDTO> Delete(Guid id)
        {
            try
            {
                var publication = _editablePublication.Delete(id);

                return Ok(publication);
            }
            catch (ObjectNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
