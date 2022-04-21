using Microsoft.AspNetCore.Http;
using NotSocialNetwork.Application.Entities.Abstract;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace NotSocialNetwork.Application.Entities
{
    public class ImageEntity : BaseEntity
    {
        public string Title { get; set; }
        [NotMapped]
        [JsonIgnore]
        public IFormFile ImageFromForm { get; set; }
        [JsonIgnore]
        public ICollection<PublicationEntity> Publications { get; set; } = new List<PublicationEntity>();
    }
}
