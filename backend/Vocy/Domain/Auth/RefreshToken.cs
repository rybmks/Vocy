using Domain.Base;

namespace Domain.Auth;

public record RefreshToken(Guid UserId, string TokenHash, DateTime ExpiresAt, DateTime CreatedAt)
    : BaseIdentity
{
    public DateTime? RevokedAt { get; set; } = null;

    public bool IsActive => RevokedAt == null && ExpiresAt > DateTime.UtcNow;
}