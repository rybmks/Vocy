using Domain.Base;

namespace Domain.User;

public record Connection : BaseIdentity
{
    public List<ConnectionUser> Members { get; init; } = [];

    public Connection()
    {
    }

    public Connection(List<ConnectionUser> Members)
    {
        this.Members = Members;
    }
}