using Microsoft.IdentityModel.Tokens;
using NotSocialNetwork.Application.Configs;
using NotSocialNetwork.Application.Entities;
using NotSocialNetwork.Application.Interfaces.Managers;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NotSocialNetwork.Services.Managers
{
    public class JwtSystem : IJwtSystem
    {
        public string GenerateToken(UserEntity user)
        {
            var secret = JwtConfig.secret;

            byte[] securityKey = Encoding.ASCII.GetBytes(secret);

            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var expirationDate = DateTime.UtcNow.AddMinutes(10);

            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("id", user.Id.ToString()),
                    new Claim("email", user.Email),
                }),

                Expires = expirationDate,

                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(securityKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var securityToken = jwtTokenHandler.CreateToken(securityTokenDescriptor);
            var token = jwtTokenHandler.WriteToken(securityToken);

            return token;
        }
    }
}
