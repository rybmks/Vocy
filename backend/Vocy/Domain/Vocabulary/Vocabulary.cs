namespace Domain.Vocabulary;

//Defining translations would be better at this level, instead of at the Word level
//Add translation entity that is pair of two words
public record Vocabulary : BaseIdentity
{
    public required Guid Owner { get; set; }
    public required List<Translate> Translates { get; init; } = new();
    Language From { get; init; }
    Language To { get; init; }

    void AddTranslate(Translate translate) 
    {
        if (translate.From.Language != From || translate.To.Language != To)
            throw new Exception();

        Translates.Add(translate);
    }
}

public record Translate
{
    public Translate(Word From, Word To)
    {
        if (From.Language == To.Language)
            throw new Exception();
        this.From = From;
        this.To = To;
    }

    public string Caption;
    public string Example;

    public Word From { get; }
    public Word To { get; }
};