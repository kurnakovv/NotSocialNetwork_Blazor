using NotSocialNetwork.Application.Configs;
using NotSocialNetwork.Application.Entities;
using NotSocialNetwork.Services.Systems;
using Xunit;

namespace NotSocialNetwork.Infrastructure.Tests.UnitTests.Services.Systems
{
    public class JwtSystemTest
    {
        private readonly UserEntity _user = new UserEntity()
        {
            Email = "user@example.com",
            Role = RoleConfig.DEFAULT_USER,
        };

        [Fact]
        public void GenerateToken_GenerateTokenIfUserIsValid_Token()
        {
            // Arrange.
            var jwtSystem = new JwtSystem();

            // Act.
            var result = jwtSystem.GenerateToken(_user);

            // Assert.
            Assert.NotNull(result);
            Assert.IsType<string>(result);
        }
    }
}
