namespace Domain.User;

public record Connection : BaseIdentity
{
    public List<ConnectionUser> Members { get; init; } = new();
}