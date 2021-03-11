using NotSocialNetwork.Application.Entities.Abstract;
using System;
using System.ComponentModel.DataAnnotations;

namespace NotSocialNetwork.Application.Entities
{
    public class UserEntity : BaseEntity, IUser
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public DateTimeOffset DateOfBirth { get; set; }
        public ImageEntity Image { get; set; }
    }
}
