namespace Domain.Vocabulary;

public record Vocabulary
{
    public required Guid Id { get; set; }
    public required Guid Owner { get; set; }
    public required List<Word> Words { get; init; } = new();
}