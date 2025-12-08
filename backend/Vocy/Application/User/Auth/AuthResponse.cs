namespace Application.User.Auth;

public record AuthResponse(Guid UserId, String AccessToken, String RefreshToken);