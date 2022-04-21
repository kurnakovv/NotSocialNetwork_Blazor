using NotSocialNetwork.Application.Entities.Abstract;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace NotSocialNetwork.Application.Entities
{
    [Table("Favorites")]
    public class FavoritesEntity
    {
        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public UserEntity User { get; set; }
        public Guid PublicationId { get; set; }
        [ForeignKey("PublicationId")]
        public PublicationEntity Publication { get; set; } 
    }
}
