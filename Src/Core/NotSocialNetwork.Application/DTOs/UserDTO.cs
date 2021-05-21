using NotSocialNetwork.Application.Entities;
using System;

namespace NotSocialNetwork.Application.DTOs
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public ImageEntity Image { get; set; }
    }
}
