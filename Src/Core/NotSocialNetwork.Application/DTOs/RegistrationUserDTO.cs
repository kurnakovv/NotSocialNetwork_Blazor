using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

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
        [JsonIgnore]
        public IFormFile Image { get; set; }
    }
}
