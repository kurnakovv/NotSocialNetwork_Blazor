using System;

namespace NotSocialNetwork.Application.DTOs
{
    public class LoginResult
    {
        public string Token { get; set; }
        public Guid UserId { get; set; }
    }
}
