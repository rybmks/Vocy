using Domain.Base;

namespace Domain.Vocabulary;

public record Translation : BaseIdentity
{
    public Translation(Word From, Word To, String Example)
    {
        if (From.Language == To.Language)
        {
            throw new Exception();
        }

        this.From = From;
        this.To = To;
        this.Example = Example;
    }

    public required Word From { get; init; }
    public required Word To { get; init; }
    public required String Example { get; set; }
}