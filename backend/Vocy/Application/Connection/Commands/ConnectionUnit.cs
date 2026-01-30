using Domain.User;

namespace Application.Connection.Commands;

public record ConnectionUnit(string Email, ConnectionRole Role);