namespace Application.Connection.Commands;

public record CreateConnectionCommand(IReadOnlyCollection<string> MembersEmails);