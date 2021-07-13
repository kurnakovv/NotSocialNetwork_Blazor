using NotSocialNetwork.Application.Entities;
using System;

namespace NotSocialNetwork.Application.DTOs
{
    public class RegistrationUserDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTimeOffset DateOfBirth = new(DateTime.Now);
        public ImageEntity Image { get; set; }
    }
}
