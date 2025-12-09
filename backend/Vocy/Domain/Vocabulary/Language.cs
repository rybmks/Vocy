using System.Runtime.Serialization;

namespace Domain.Vocabulary;

public enum Language
{
    [EnumMember(Value = "ua")] Ua,
    [EnumMember(Value = "en")] En,
    [EnumMember(Value = "pl")] Pl
}