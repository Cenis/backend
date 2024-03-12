namespace CounTrivia.Entities;

public record class AnswerOption
{
    public Guid Id { get; set; }
    
    public bool IsCorrect { get; set; }

    public string DisplayText { get; set; } = default!;

    public string FlagUrl { get; set; } = default!;
}