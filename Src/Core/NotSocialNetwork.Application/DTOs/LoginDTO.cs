using System.ComponentModel.DataAnnotations;

namespace NotSocialNetwork.Application.DTOs
{
    public class LoginDTO
    {
        [EmailAddress]
        public string Email { get; set; }
    }
}
