namespace Domain.User;

public record User(Guid Id, String Name, Email Email, String Password);