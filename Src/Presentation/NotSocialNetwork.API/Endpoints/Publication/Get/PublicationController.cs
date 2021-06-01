using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotSocialNetwork.Application.DTOs;
using NotSocialNetwork.Application.Exceptions;
using NotSocialNetwork.Application.Interfaces.UseCases.Publication;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;

namespace NotSocialNetwork.API.Endpoints.Publication.Get
{
    [Route("api/publication")]
    [ApiController]
    [Authorize]
    public class PublicationController : ControllerBase
    {
        public PublicationController(
            IGetablePublication getablePublication,
            IMapper mapper)
        {
            _getablePublication = getablePublication;
            _mapper = mapper;
        }

        private readonly IGetablePublication _getablePublication;
        private readonly IMapper _mapper;

        /// <summary>
        /// Get all publications by pagination.
        /// </summary>
        /// <returns>Publications.</returns>
        [HttpGet("index={index}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(IEnumerable<PublicationDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [SwaggerOperation(
            Summary = "Get all by pagination.",
            Description = "Get all publications by pagination."
        )]
        public ActionResult<IEnumerable<PublicationDTO>> Get(int index = 0)
        {
            try
            {
                var publicationsEntity = _getablePublication.GetByPagination(index);

                var publicationsDTO =
                    _mapper.Map<IEnumerable<PublicationDTO>>(publicationsEntity);

                return Ok(publicationsDTO);
            }
            catch (ObjectNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get publication by id.
        /// </summary>
        /// <param name="id">Publication id.</param>
        /// <returns>Publication.</returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(PublicationDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [SwaggerOperation(
            Summary = "Get by id.",
            Description = "Get publication by id."
        )]
        public ActionResult<PublicationDTO> Get(Guid id)
        {
            try
            {
                var publicationEntity = _getablePublication.GetById(id);

                var publicationDTO =
                    _mapper.Map<PublicationDTO>(publicationEntity);

                return Ok(publicationDTO);
            }
            catch (ObjectNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Get all publications by author id.
        /// </summary>
        /// <param name="authorId">Author id</param>
        /// <returns>Publications</returns>
        [HttpGet("author={authorId}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(PublicationDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [SwaggerOperation(
            Summary = "Get all by author id.",
            Description = "Get all publications by author id."
        )]
        public ActionResult<IEnumerable<PublicationDTO>> GetAllByAuthor(Guid authorId)
        {
            try
            {
                var publicationsEntity = _getablePublication.GetAllByAuthorId(authorId);

                var publicationsDTO =
                    _mapper.Map<IEnumerable<PublicationDTO>>(publicationsEntity);

                return Ok(publicationsDTO);
            }
            catch (ObjectNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
