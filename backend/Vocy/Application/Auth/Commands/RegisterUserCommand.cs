namespace Application.Auth.Commands;

public record class RegisterUserCommand(String Name, String Email, String Password);