using NotSocialNetwork.Application.DTOs.Favorite;

namespace NotSocialNetwork.Application.Interfaces.UseCases.Favorite
{
    public interface ILikeableFavorite
    {
        FavoriteResultDTO LikeOrUnlike(FavoriteDTO favoriteDTO);
    }
}
