using StoreManager.Infrastructure.User.Model;

namespace StoreManager.Domain.Auth.Tokens.RefreshToken.Model
{
    public class RefreshTokenModel
    {
        public Guid Id { get; set; }
        public required string Token { get; set; }
        public int UserId { get; set; }
        public DateTime ExpiresOnUtc { get; set; }
        public required UserModel User { get; set; }
    }
}
