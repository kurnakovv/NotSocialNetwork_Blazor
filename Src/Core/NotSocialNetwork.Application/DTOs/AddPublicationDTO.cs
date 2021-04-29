using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;

namespace NotSocialNetwork.Application.DTOs
{
    public class AddPublicationDTO
    {
        public string Title { get; set; }
        public Guid AuthorId { get; set; }
        public string Text { get; set; } // Convert from string to file.
        [JsonIgnore]
        public IEnumerable<IFormFile> Images { get; set; }
    }
}
