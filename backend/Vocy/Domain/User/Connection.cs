namespace Domain.User;

public record Connection
{
    public Guid Id { get; init; }
    public List<ConnectionUser> Members { get; init; } = new();
}