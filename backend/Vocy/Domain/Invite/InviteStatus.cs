using System.Runtime.Serialization;

namespace Domain.Invite;

public enum InviteStatus
{
    [EnumMember(Value = "pending")] Pending,
    [EnumMember(Value = "accepted")] Accepted,
    [EnumMember(Value = "rejected")] Rejected
}