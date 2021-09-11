using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NotSocialNetwork.Application.DTOs;
using NotSocialNetwork.Application.Exceptions;
using NotSocialNetwork.Application.Interfaces.UseCases.Favorite;
using System;
using System.Collections.Generic;

namespace NotSocialNetwork.API.Endpoints.Favorite.Get
{
    [Route("api/favorite")]
    [ApiController]
    public class FavoriteController : ControllerBase
    {
        public FavoriteController(
            IGetableFavorite getableFavorite,
            IMapper mapper)
        {
            _getableFavorite = getableFavorite;
            _mapper = mapper;
        }

        private readonly IGetableFavorite _getableFavorite;
        private readonly IMapper _mapper;

        [HttpGet("{authorId}")]
        public ActionResult<IEnumerable<PublicationDTO>> Get(Guid authorId)
        {
            try
            {
                var publicationsEntity = _getableFavorite.GetPublicationsWithFavorites(authorId);

                var publicationsDTO =
                    _mapper.Map<IEnumerable<PublicationDTO>>(publicationsEntity);

                return Ok(publicationsDTO);
            }
            catch(ObjectNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch(FavoritesNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
