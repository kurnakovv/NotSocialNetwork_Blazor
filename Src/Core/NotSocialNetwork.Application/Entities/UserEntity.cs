using NotSocialNetwork.Application.Entities.Abstract;
using System;
using System.Collections.Generic;

namespace NotSocialNetwork.Application.Entities
{
    public class UserEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTimeOffset DateOfBirth { get; set; }
        public ImageEntity Image { get; set; }
        public string Role { get; set; }
        public ICollection<PublicationEntity> Favorites { get; set; }
        public List<FavoritesEntity> FavoritesEntities { get; set; }
    }
}
