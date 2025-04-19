using Castle.Core.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Memory;
using StoreManager.Infrastructure.User.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using StoreManager.Infrastructure.Auth.Tokens;

namespace StoreManager.Tests.Auth.Tokens.AccessToken.Generator
{
    public sealed class AccessTokenGeneratorTest : IAsyncLifetime
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        private Microsoft.Extensions.Configuration.IConfiguration _mockConfig;
        private AccessTokenGenerator _generator;
        private MockValidator _validator;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

        private static readonly string USERNAME = "test";
        private static readonly UserRole ROLE = UserRole.ADMIN;

        [Fact(DisplayName = "GenerateToken - Valid")]
        public void GenerateToken_ValidTest()
        {
            var ex = Record.Exception(() =>
            {
                var token = _generator.GenerateToken(USERNAME, ROLE.ToString());
                Assert.NotNull(token);
                Assert.NotEmpty(token);
                var validatedToken = _validator.ValidateToken(token);
                Assert.NotNull(validatedToken);
                Assert.True(validatedToken is JwtSecurityToken);

                var jwtToken = validatedToken as JwtSecurityToken;
                Assert.NotNull(jwtToken);

                var nameClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Name);
                var roleClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "role");

                Assert.NotNull(nameClaim);
                Assert.NotNull(roleClaim);

                Assert.Equal(USERNAME, nameClaim.Value);

                Assert.Equal(ROLE.ToString(), roleClaim.Value);


            });
            Assert.Null(ex);

        }

        public async Task InitializeAsync()
        {
            var configValues = new Dictionary<string, string>
           {
            { "JwtSettings:ExpiryIntervalInMinutes", "60" },
            { "JwtSettings:Issuer", "test-issuer" },
            { "JwtSettings:Audience", "test-audience" }
            };
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
            _mockConfig = new ConfigurationBuilder().Add(new MemoryConfigurationSource { InitialData = configValues }).Build();
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.

            _generator = new AccessTokenGenerator(_mockConfig);
            var secret = Environment.GetEnvironmentVariable("JWT_SECRET", EnvironmentVariableTarget.User) ?? throw new Exception();
            _validator = new MockValidator(secret, _mockConfig);
            await Task.CompletedTask;
        }

        public async Task DisposeAsync()
        {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            _mockConfig = null;
            _generator = null;
            _validator = null;
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

            await Task.CompletedTask;
        }
    }
}
