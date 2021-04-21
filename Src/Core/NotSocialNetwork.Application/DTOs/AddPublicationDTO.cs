using System;

namespace NotSocialNetwork.Application.DTOs
{
    public class AddPublicationDTO
    {
        public string Title { get; set; }
        public Guid AuthorId { get; set; }
        public string Text { get; set; } // Convert from string to file.
    }
}
