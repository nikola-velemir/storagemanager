using StoreManager.Domain.User.Model;
using StoreManager.Infrastructure.User.Model;

namespace StoreManager.Domain.Auth.Tokens.RefreshToken.Model
{
    public class RefreshTokenModel
    {
        public Guid Id { get; set; }
        public required string Token { get; set; }
        public int UserId { get; set; }
        public DateTime ExpiresOnUtc { get; set; }
        public required User.Model.User User { get; set; }
    }
}
