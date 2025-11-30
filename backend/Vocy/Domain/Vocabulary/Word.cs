namespace Domain.Vocabulary;

//Translation to which language? How handle A = B & B = A cases? What if languages is more than two?
public record Word(string Value, Language Language) : BaseIdentity;