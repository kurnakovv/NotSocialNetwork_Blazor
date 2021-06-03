using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotSocialNetwork.Application.DTOs;
using NotSocialNetwork.Application.Entities;
using NotSocialNetwork.Application.Exceptions;
using NotSocialNetwork.Application.Interfaces.UseCases.Publication;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace NotSocialNetwork.API.Endpoints.Publication.Add
{
    [Route("api/publication")]
    [ApiController]
    [Authorize]
    public class PublicationController : ControllerBase
    {
        public PublicationController(
            IAddablePublicationAsync addablePublication,
            IMapper mapper)
        {
            _addablePublication = addablePublication;
            _mapper = mapper;
        }

        private readonly IAddablePublicationAsync _addablePublication;
        private readonly IMapper _mapper;

        /// <summary>
        /// Add publication.
        /// </summary>
        /// <param name="publication">Publication parameters.</param>
        /// <returns>Publication.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(AddPublicationDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [SwaggerOperation(
            Summary = "Add.",
            Description = "Add publication."
        )]
        public async Task<ActionResult<AddPublicationDTO>> Add(AddPublicationDTO publication)
        {
            try
            {
                var publicationEntity =
                    _mapper.Map<PublicationEntity>(publication);

                await _addablePublication.AddAsync(publicationEntity);

                return Ok(publication);
            }
            catch (ObjectAlreadyExistException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ObjectNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
