using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotSocialNetwork.Application.Configs;
using NotSocialNetwork.Application.DTOs;
using NotSocialNetwork.Application.Entities;
using NotSocialNetwork.Application.Exceptions;
using NotSocialNetwork.Application.Interfaces.UseCases.Publication;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Threading.Tasks;

namespace NotSocialNetwork.API.Endpoints.Admin.Publication.Edit
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = RoleConfig.ADMINISTRATOR)]
    public class AdminPublicationController : ControllerBase
    {
        public AdminPublicationController(
            IEditablePublicationAsync editablePublication,
            IGetablePublication getablePublication,
            IMapper mapper)
        {
            _editablePublication = editablePublication;
            _getablePublication = getablePublication;
            _mapper = mapper;
        }

        private readonly IEditablePublicationAsync _editablePublication;
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
        public async Task<ActionResult<PublicationDTO>> Update(UpdatePublicationDTO publication)
        {
            try
            {
                var publicationEntityOld = _getablePublication.GetById(publication.Id);
                var publicationEntity =
                    _mapper.Map<UpdatePublicationDTO, PublicationEntity>(publication, publicationEntityOld);

                await _editablePublication.UpdateAsync(publicationEntity);

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
        public async Task<ActionResult<PublicationDTO>> Delete(Guid id)
        {
            try
            {
                var publication = await _editablePublication.DeleteAsync(id);

                return Ok(publication);
            }
            catch (ObjectNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
