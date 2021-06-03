using Microsoft.IdentityModel.Tokens;
using NotSocialNetwork.Application.Configs;
using NotSocialNetwork.Application.Entities;
using NotSocialNetwork.Application.Interfaces.Systems;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NotSocialNetwork.Services.Systems
{
    public class JwtSystem : IJwtSystem
    {
        private byte[] _securityKey = Encoding.ASCII.GetBytes(JwtConfig.SECRET);
        private DateTime _expirationDate = DateTime.UtcNow.AddDays(2);
        private readonly JwtSecurityTokenHandler _jwtTokenHandler = new JwtSecurityTokenHandler();

        public string GenerateToken(UserEntity user)
        {
            var securityTokenDescriptor = InitSecurityTokenDescriptor(user);

            var securityToken = _jwtTokenHandler.CreateToken(securityTokenDescriptor);
            var token = _jwtTokenHandler.WriteToken(securityToken);

            return token;
        }

        private SecurityTokenDescriptor InitSecurityTokenDescriptor(UserEntity user)
        {
            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("id", user.Id.ToString()),
                    new Claim("email", user.Email),
                }),

                Expires = _expirationDate,

                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(_securityKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            return securityTokenDescriptor;
        }
    }
}
