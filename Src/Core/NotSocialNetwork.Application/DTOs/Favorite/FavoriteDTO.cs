using System;

namespace NotSocialNetwork.Application.DTOs.Favorite
{
    public class FavoriteDTO
    {
        public Guid UserId { get; set; }
        public Guid PublicationId { get; set; }
    }
}
