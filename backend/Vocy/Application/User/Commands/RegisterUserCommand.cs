namespace Application.User.Commands;

public record class RegisterUserCommand(String Name, String Email, String Password);