using Domain.Base;

namespace Domain.Vocabulary;

public record Vocabulary : BaseIdentity
{
    public required Guid Owner { get; set; }
    public required List<Translation> Translations { get; init; } = [];

    public void AddTranslation(Translation translation)
    {
        if (translation.From.Language != From || translation.To.Language != To)
            throw new InvalidOperationException("Translation languages do not match vocabulary direction.");


        this.Translations.Add(translation);
    }

    public void RemoveTranslation(Guid translationId)
    {
        var translation = Translations.SingleOrDefault(t => t.Id == translationId);

        if (translation == null)
            throw new Exception();

        Translations.Remove(translation);
    }

    public required Language From { get; set; }
    public required Language To { get; set; }
}