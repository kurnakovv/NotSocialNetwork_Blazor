using NotSocialNetwork.Application.Entities.Abstract;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NotSocialNetwork.Application.Entities
{
    public class PublicationEntity : BaseEntity, IPublication
    {
        [Required]
        public string Title { get; set; }
        public ICollection<ImageEntity> PublicationImages { get; set; }
        [Required]
        public UserEntity Author { get; set; }
    }
}
