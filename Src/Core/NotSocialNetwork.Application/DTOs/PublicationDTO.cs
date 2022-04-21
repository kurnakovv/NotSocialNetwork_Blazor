using NotSocialNetwork.Application.Entities;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace NotSocialNetwork.Application.DTOs
{
    public class PublicationDTO
    {
        public Guid Id { get; set; }
        public UserDTO Author { get; set; }
        public string Text { get; set; }
        public IEnumerable<string> ImagePaths { get; set; }
    }
}
