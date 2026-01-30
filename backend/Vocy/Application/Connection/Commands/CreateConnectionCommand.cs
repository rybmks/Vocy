namespace Application.Connection.Commands;

public record CreateConnectionCommand(IReadOnlyCollection<ConnectionUnit> ConnectionUnits);