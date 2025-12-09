using Domain.Base;

namespace Domain.Invite;

public record Invite(User.User From, User.User To, InviteStatus Status) : BaseIdentity;