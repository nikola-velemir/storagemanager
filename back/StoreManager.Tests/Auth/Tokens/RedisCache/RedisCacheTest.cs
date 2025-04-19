using Moq;
using StackExchange.Redis;
using System.Threading.Tasks;
using StoreManager.Infrastructure.Auth.Tokens;

namespace StoreManager.Tests.Auth.Tokens.ReishCahce
{
    public sealed class RedisCacheTest : IAsyncLifetime
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        private Mock<IDatabase> _db;
        private RedisCacheService _redis;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

        private static readonly string REVOKED_JTI = "12345";
        private static readonly string JTI = "sdjaksl";
        private static readonly DateTime VALID_EXPIRY = DateTime.UtcNow.AddMinutes(-10);
        private static readonly DateTime INVALID_EXPIRY = DateTime.UtcNow.AddMinutes(10);

        [Fact(DisplayName = "Revoke token - Expired")]
        public async Task RevokeToken_ExpiredTest()
        {
            var expectedExpiry = VALID_EXPIRY - DateTime.UtcNow;
            await _redis.RevokeToken(JTI, VALID_EXPIRY);
            _db.Verify(db => db.StringSetAsync(It.Is<RedisKey>(key => key == $"revoked_token:{JTI}"),
                It.Is<RedisValue>(val => val == "revoked"),
                It.IsAny<TimeSpan>(),
                It.IsAny<When>(),
                It.IsAny<CommandFlags>()
            ), Times.Never);
        }
        [Fact(DisplayName = "Revoke token - Did not expire")]
        public async Task RevokeToken_DidNotExpire()
        {
            DateTime futureExpiry = DateTime.UtcNow.AddMinutes(10);

            var expectedExpiry = futureExpiry - DateTime.UtcNow;
            await _redis.RevokeToken(JTI, futureExpiry);
            _db.Verify(db => db.StringSetAsync(
                   It.Is<RedisKey>(key => key == $"revoked_token:{JTI}"),
                   It.Is<RedisValue>(val => val == "revoked"),
                   It.Is<TimeSpan>(ts => ts.TotalSeconds <= 600 && ts.TotalSeconds > 0),
                   It.IsAny<When>(),
                   It.IsAny<CommandFlags>()
            ), Times.Never);
        }
        [Fact(DisplayName = "Is revoked - Revoked JTI")]
        public async Task IsRevoked_RevokedJTITest()
        {
            var isRevoked = await _redis.IsTokenRevoked(REVOKED_JTI);
            Assert.True(isRevoked);
        }
        [Fact(DisplayName = "Is revoked - Revoked JTI")]
        public async Task IsRevoked_UnrevokedJTITest()
        {
            var isRevoked = await _redis.IsTokenRevoked(JTI);
            Assert.False(isRevoked);
        }

        public async Task InitializeAsync()
        {
            _db = new Mock<IDatabase>();
            var multiplexer = new Mock<IConnectionMultiplexer>();
            multiplexer.Setup(m => m.GetDatabase(It.IsAny<int>(), It.IsAny<object>())).Returns(_db.Object);
            MockRedisDb();
            _redis = new RedisCacheService(multiplexer.Object);
            await Task.CompletedTask;
        }
        private void MockRedisDb()
        {
            _db.Setup(db => db.KeyExistsAsync(It.Is<RedisKey>(key => key == $"revoked_token:{REVOKED_JTI}"), It.IsAny<CommandFlags>()))
               .ReturnsAsync(true);
            _db.Setup(db => db.KeyExistsAsync(It.Is<RedisKey>(key => key == $"revoked_token:{JTI}"), It.IsAny<CommandFlags>())).ReturnsAsync(false);

            _db.Setup(db => db.StringSetAsync(It.Is<RedisKey>(key => key == $"revoked_token:{JTI}"),
                   It.Is<RedisValue>(val => val == "revoked"),
                   It.Is<TimeSpan>(ts => ts.TotalSeconds <= 600 && ts.TotalSeconds > 0),
                   It.IsAny<When>(),
                   It.IsAny<CommandFlags>())).ReturnsAsync(true);

        }
        public async Task DisposeAsync()
        {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            _db = null;
            _redis = null;
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

            await Task.CompletedTask;
        }
    }
}
