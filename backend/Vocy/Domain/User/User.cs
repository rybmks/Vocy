namespace Domain.User;

public record User(string Name, string Email, string Password) : BaseIdentity;