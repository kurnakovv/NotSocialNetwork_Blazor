using NotSocialNetwork.Application.Entities;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace NotSocialNetwork.Application.DTOs
{
    public class PublicationDTO
    {
        public Guid Id { get; set; }
        [JsonIgnore]
        public UserEntity Author { get; set; }
        public Guid AutorId { get; set; }
        public string Text { get; set; }
        public IEnumerable<string> ImagePaths { get; set; }
    }
}
