using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using NotSocialNetwork.Application.Configs;
using NotSocialNetwork.Application.Interfaces.Services;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotSocialNetwork.API
{
    public class JwtAuthenticationMiddleware
    {
        public JwtAuthenticationMiddleware(
            RequestDelegate next)
        {
            _next = next;
        }

        private readonly RequestDelegate _next;

        public async Task Invoke(HttpContext context, IUserService userService)
        {
            ValidateToken(context, userService);

            await _next(context);
        }

        private void ValidateToken(HttpContext context, IUserService userService)
        {
            var authHeader = context.Request.Headers["Authorization"]
                                   .FirstOrDefault();

            if (authHeader != null)
            {
                var secret = JwtConfig.secret;

                if (string.IsNullOrWhiteSpace(secret))
                {
                    throw new ArgumentException("Secret not set.");
                }

                var tokenHeader = new JwtSecurityTokenHandler();

                byte[] key = Encoding.ASCII.GetBytes(secret);

                var token = authHeader.Split(" ").Last();

                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateAudience = false,
                    ValidateIssuer = false,
                };

                tokenHeader.ValidateToken(
                    token,
                    validationParameters,
                    out SecurityToken validatedToken);

                var jwtToken = validatedToken as JwtSecurityToken;

                var userId = Guid.Parse(jwtToken.Claims
                    .First(c => c.Type == "id")
                    .Value);

                context.Items["User"] = userService.GetById(userId);
            }
        }
    }
}
