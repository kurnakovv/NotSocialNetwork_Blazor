using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NotSocialNetwork.API.Attributes;
using NotSocialNetwork.Application.DTOs;
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
            IPublicationService publicationService,
            IMapper mapper)
        {
            _publicationService = publicationService;
            _mapper = mapper;
        }

        private readonly IPublicationService _publicationService;
        private readonly IMapper _mapper;

        [HttpGet]
        public IEnumerable<PublicationDTO> Get()
        {
            var publicationsEntitie = _publicationService.GetAll();

            var publicationsDTO =
                _mapper.Map<IEnumerable<PublicationDTO>>(publicationsEntitie);

            return publicationsDTO;
        }

        [HttpGet("{id}")]
        public ActionResult<PublicationDTO> Get(Guid id)
        {
            try
            {
                var publicationEntity = _publicationService.GetById(id);
                
                var publicationDTO = 
                    _mapper.Map<PublicationDTO>(publicationEntity);

                return publicationDTO;
            }
            catch (ObjectNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult<AddPublicationDTO> Add(AddPublicationDTO publication)
        {
            try
            {
                var publicationEntity =
                    _mapper.Map<PublicationEntity>(publication);

                _publicationService.Add(publicationEntity);

                return Ok(publication);
            }
            catch (ObjectAlreadyExistException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
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

        [HttpDelete]
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
    }
}
