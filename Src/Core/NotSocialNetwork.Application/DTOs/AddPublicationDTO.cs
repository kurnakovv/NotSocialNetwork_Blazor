using System;

namespace NotSocialNetwork.Application.DTOs
{
    public class AddPublicationDTO
    {
        public string Title { get; set; }
        public Guid AuthorId { get; set; }
        // TODO: Add Text and Images.
    }
}
