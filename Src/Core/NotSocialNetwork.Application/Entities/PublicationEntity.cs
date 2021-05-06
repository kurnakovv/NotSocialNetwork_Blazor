using NotSocialNetwork.Application.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NotSocialNetwork.Application.Entities
{
    public class PublicationEntity : BaseEntity, IPublication
    {
        public PublicationEntity()
        {
            Images = new List<ImageEntity>();
        }

        public ICollection<ImageEntity> Images { get; set; }
        [Required]
        public UserEntity Author { get; set; }
        public Guid AuthorId { get; set; }
        public string Text { get; set; } // Convert from string to file.
    }
}
