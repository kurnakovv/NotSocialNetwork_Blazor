using Microsoft.IdentityModel.Tokens;
using NotSocialNetwork.Application.Configs;
using NotSocialNetwork.Application.Entities;
using NotSocialNetwork.Application.Interfaces.Systems;
using System;
using System.Collections.Generic;
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
        private ICollection<Claim> _claims = new List<Claim>();

        public string GenerateToken(UserEntity user)
        {
            var securityTokenDescriptor = InitSecurityTokenDescriptor(user);

            var securityToken = _jwtTokenHandler.CreateToken(securityTokenDescriptor);
            var token = _jwtTokenHandler.WriteToken(securityToken);

            return token;
        }

        private SecurityTokenDescriptor InitSecurityTokenDescriptor(UserEntity user)
        {
            AddClaims(user);

            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(_claims),

                Expires = _expirationDate,

                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(_securityKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            return securityTokenDescriptor;
        }

        private void AddClaims(UserEntity user)
        {
            CleanClaims();

            _claims.Add(new Claim("id", user.Id.ToString()));
            _claims.Add(new Claim("email", user.Email));
            _claims.Add(new Claim("role", user.Role));
        }

        private void CleanClaims()
        {
            _claims = new List<Claim>();
        }
    }
}
