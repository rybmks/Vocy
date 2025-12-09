namespace Application.Auth.Helpers;

public record AuthResponse(Guid UserId, String AccessToken, String RefreshToken);