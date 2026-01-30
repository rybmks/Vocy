using Domain.Base;

namespace Domain.User;

public record ConnectionUser
{
    public Guid UserId { get; init; }
    public ConnectionRole Role { get; init; }

    public User User { get; init; } = null!;

    public ConnectionUser(Guid userId, ConnectionRole role)
    {
        UserId = userId;
        Role = role;
    }

    private ConnectionUser()
    {
    }
}