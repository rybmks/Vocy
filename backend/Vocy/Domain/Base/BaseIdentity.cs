namespace Domain.Base;

public abstract record BaseIdentity
{
    //Todo: change identity creation logic
    public Guid Id { get; init; } = GenerateIdentity();

    public static Guid GenerateIdentity() => Guid.NewGuid();
}