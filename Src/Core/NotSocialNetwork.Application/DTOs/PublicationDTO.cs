using NotSocialNetwork.Application.Entities;
using System;
using System.Collections.Generic;

namespace NotSocialNetwork.Application.DTOs
{
    public class PublicationDTO
    {
        public Guid Id { get; set; }
        public UserEntity Author { get; set; }
        public string Text { get; set; } // TODO: Convert string to file.
        public IEnumerable<string> ImagePaths { get; set; }
    }
}
