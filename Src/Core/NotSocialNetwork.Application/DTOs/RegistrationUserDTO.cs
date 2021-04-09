using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace NotSocialNetwork.Application.DTOs
{
    public class RegistrationUserDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public DateTimeOffset DateOfBirth = new(DateTime.Now);
        public IFormFile Files { get; set; }
    }
}
