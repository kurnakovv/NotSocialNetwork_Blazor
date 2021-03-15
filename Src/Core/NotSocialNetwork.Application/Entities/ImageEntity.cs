using Microsoft.AspNetCore.Http;
using NotSocialNetwork.Application.Entities.Abstract;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace NotSocialNetwork.Application.Entities
{
    public class ImageEntity : BaseEntity
    {
        [JsonIgnore]
        public string Title { get; set; }
        [NotMapped]
        [JsonIgnore]
        public IFormFile ImageFromForm { get; set; }
    }
}
