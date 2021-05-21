using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using NotSocialNetwork.Application.Entities;

namespace NotSocialNetwork.Application.DTOs
{
    public class AddPublicationDTO
    {
        public Guid AuthorId { get; set; }
        public string Text { get; set; } // Convert from string to file.
        public IEnumerable<ImageEntity> Images { get; set; }
    }
}
