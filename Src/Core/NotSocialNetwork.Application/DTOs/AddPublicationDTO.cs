using NotSocialNetwork.Application.Entities;
using System;
using System.Collections.Generic;

namespace NotSocialNetwork.Application.DTOs
{
    public class AddPublicationDTO
    {
        public Guid AuthorId { get; set; }
        public string Text { get; set; } // Convert from string to file.
        public IEnumerable<ImageEntity> Images { get; set; }
    }
}
