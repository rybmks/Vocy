namespace Domain.Base;

public abstract record BaseIdentity
{
    public Guid Id { get; init; }
}