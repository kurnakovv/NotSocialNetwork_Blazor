using Microsoft.AspNetCore.Mvc;
using NotSocialNetwork.API.Attributes;
using NotSocialNetwork.Application.Entities;
using NotSocialNetwork.Application.Exceptions;
using NotSocialNetwork.Application.Interfaces.Services;
using System;
using System.Collections.Generic;

namespace NotSocialNetwork.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [JwtAuthorize]
    public class PublicationController : ControllerBase
    {
        public PublicationController(
            IPublicationService publicationService)
        {
            _publicationService = publicationService;
        }

        private readonly IPublicationService _publicationService;

        [HttpGet]
        public IEnumerable<PublicationEntity> Get()
        {
            return _publicationService.GetAll();
        }

        [HttpGet("{id}")]
        public ActionResult<PublicationEntity> Get(Guid id)
        {
            try
            {
                return _publicationService.GetById(id);
            }
            catch (ObjectNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult<PublicationEntity> Add(PublicationEntity publication)
        {
            try
            {
                // TODO: Convert PublicationEntity -> AddPublicationDTO.
                _publicationService.Add(publication);

                return Ok(publication);
            }
            catch (ObjectAlreadyExistException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public ActionResult<PublicationEntity> Update(PublicationEntity publication)
        {
            try
            {
                // TODO: Convert PublicationEntity -> UpdatePublicationDTO.
                _publicationService.Update(publication);

                return Ok(publication);
            }
            catch (ObjectNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete]
        public ActionResult<UserEntity> Delete(Guid id)
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
    }
}
