using CounTrivia.Entities;

namespace CounTrivia.Services.AnswerGenerator;

public class FreeTextAnswerGenerator : IAnswerOptionGenerator
{
    public EClosedQuestionAnswerDisplayType AnswerDisplayType => EClosedQuestionAnswerDisplayType.None;
    
    public AnswerOption GenerateAnswerOption(Country country, bool isCorrect)
    {
        return new AnswerOption
        {
            Id = country.Id,
            DisplayText = country.Capital,  // TODO: Make field "Capital" dynamic!
            FlagUrl = string.Empty,
            IsCorrect = isCorrect,
        };
    }
}