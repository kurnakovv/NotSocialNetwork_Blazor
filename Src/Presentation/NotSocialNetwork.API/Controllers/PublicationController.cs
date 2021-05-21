using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using NotSocialNetwork.API.Attributes;
using NotSocialNetwork.Application.DTOs;
using NotSocialNetwork.Application.Entities;
using NotSocialNetwork.Application.Exceptions;
using NotSocialNetwork.Application.Interfaces.Systems;
using NotSocialNetwork.Application.Interfaces.Services;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using NotSocialNetwork.Application.Interfaces.Facades;
using NotSocialNetwork.Application.Configs;

namespace NotSocialNetwork.API.Controllers
{
    /// <summary>
    /// Publication.
    /// </summary>
    [Route("api/publication")]
    [ApiController]
    public class PublicationController : ControllerBase
    {
        public PublicationController(
            IPublicationService publicationService,
            IMapper mapper,
            IImageRepositorySystem imageRepositorySystem)
        {
            _publicationService = publicationService;
            _mapper = mapper;
            _imageRepositorySystem = imageRepositorySystem;
        }

        private readonly IPublicationService _publicationService;
        private readonly IMapper _mapper;
        private readonly IImageRepositorySystem _imageRepositorySystem;

        /// <summary>
        /// Get all publications by pagination.
        /// </summary>
        /// <returns>Publications.</returns>
        [HttpGet("index={index}")]
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
                var publicationsEntity = _publicationService.GetByPagination(index);

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
                var publicationEntity = _publicationService.GetById(id);
                
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
                var publicationsEntity = _publicationService.GetAllByAuthorId(authorId);

                var publicationsDTO =
                    _mapper.Map<IEnumerable<PublicationDTO>>(publicationsEntity);

                return Ok(publicationsDTO);
            }
            catch (ObjectNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Add publication.
        /// </summary>
        /// <param name="publication">Publication parameters.</param>
        /// <returns>Publication.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(AddPublicationDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [JwtAuthorize]
        [SwaggerOperation(
            Summary = "Add.",
            Description = "Add publication."
        )]
        public ActionResult<AddPublicationDTO> Add(/*[FromForm]*/AddPublicationDTO publication)
        {
            try
            {
                var publicationEntity =
                    _mapper.Map<PublicationEntity>(publication);

                var publicationEntityWithImages = AddImages(publication, publicationEntity);
                _publicationService.Add(publicationEntityWithImages);

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

        /// <summary>
        /// Update publication.
        /// </summary>
        /// <param name="publication">Publication parameters.</param>
        /// <returns>Publication.</returns>
        [HttpPut]
        [ProducesResponseType(typeof(PublicationDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [JwtAuthorize]
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

                var publicationEntity = _publicationService.GetById(publication.Id);
                publicationEntity.Text = publication.Text;



                _publicationService.Update(publicationEntity);

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
        [JwtAuthorize]
        [SwaggerOperation(
            Summary = "Delete by id.",
            Description = "Delete publication by id."
        )]
        public ActionResult<PublicationDTO> Delete(Guid id)
        {
            try
            {
                var publication = _publicationService.Delete(id);

                return Ok(publication);
            }
            catch (ObjectNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // TODO: Transfer this logic in PublicationService.
        private PublicationEntity AddImages(AddPublicationDTO publication, PublicationEntity publicationEntity)
        {
            foreach(ImageEntity imageEntity in publication.Images)
            {
                var newImage = _imageRepositorySystem.TrySave(imageEntity);

                publicationEntity.Images.Add(newImage);
            }

            return publicationEntity;
        }
    }
}
