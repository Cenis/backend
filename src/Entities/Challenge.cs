using System.ComponentModel.DataAnnotations.Schema;
using CounTrivia.Services.AnswerGenerator;

namespace CounTrivia.Entities;

public class Challenge
{
    public Guid Id { get; set; }

    public string Task { get; init; } = default!;

    public int MaxPoints { get; set; } = 100;

    public EAnswerFormat AnswerFormat { get; init; }

    [ForeignKey("CorrectAnswerForChallenge")]
    public List<Country> CorrectAnswers { get; init; } = default!;

    [ForeignKey("AlternativeAnswerForChallenge")]
    public List<Country> AlternativeAnswers { get; init; } = default!;

    [NotMapped]
    public IAnswerOptionGenerator AnswerOptionGenerator { get; init; } = default!;
}