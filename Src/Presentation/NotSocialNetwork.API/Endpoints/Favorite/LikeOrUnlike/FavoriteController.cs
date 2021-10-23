using Microsoft.AspNetCore.Mvc;
using NotSocialNetwork.Application.DTOs.Favorite;
using NotSocialNetwork.Application.Exceptions;
using NotSocialNetwork.Application.Interfaces.UseCases.Favorite;

namespace NotSocialNetwork.API.Endpoints.Favorite.LikeOrUnlike
{
    [Route("api/favorite")]
    [ApiController]
    public class FavoriteController : ControllerBase
    {
        public FavoriteController(
            ILikeableFavorite likeableFavorite)
        {
            _likeableFavorite = likeableFavorite;
        }

        private readonly ILikeableFavorite _likeableFavorite;

        [HttpPost]
        public ActionResult<FavoriteResultDTO> LikeOrUnlike(FavoriteDTO favoriteDTO)
        {
            try
            {
                return Ok(_likeableFavorite.LikeOrUnlike(favoriteDTO));
            }
            catch (ObjectNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
