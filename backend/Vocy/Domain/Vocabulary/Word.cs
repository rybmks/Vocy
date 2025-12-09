using Domain.Base;

namespace Domain.Vocabulary;

public record Word(string Value, Language Language) : BaseIdentity;