using NotSocialNetwork.Application.Entities.Abstract;
using System;
using System.Collections.Generic;

namespace NotSocialNetwork.Application.Entities
{
    public class PublicationEntity : BaseEntity
    {
        public ICollection<ImageEntity> Images { get; set; } = new List<ImageEntity>();
        public UserEntity Author { get; set; }
        public Guid AuthorId { get; set; }
        public string Text { get; set; }
    }
}
