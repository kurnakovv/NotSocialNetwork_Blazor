using NotSocialNetwork.Application.Entities.Abstract;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NotSocialNetwork.Application.Entities
{
    public class PublicationEntity : BaseEntity, IPublication
    {
        [Required]
        public string Title { get; set; }
        public ICollection<string> Images { get; set; }
        [Required]
        public IUser Author { get; set; }
    }
}
