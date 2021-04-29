using NotSocialNetwork.Application.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NotSocialNetwork.Application.Entities
{
    public class PublicationEntity : BaseEntity, IPublication
    {
        public PublicationEntity()
        {
            PublicationImages = new List<PublicationImageEntity>();    
        }

        [Required]
        public string Title { get; set; }
        [NotMapped]
        public ICollection<PublicationImageEntity> PublicationImages { get; set; }
        [Required]
        public UserEntity Author { get; set; }
        public Guid AuthorId { get; set; }
        public string Text { get; set; } // Convert from string to file.
    }
}
