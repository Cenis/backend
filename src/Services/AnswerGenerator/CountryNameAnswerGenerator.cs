using CounTrivia.Entities;

namespace CounTrivia.Services.AnswerGenerator;

public class CountryNameAnswerGenerator : IAnswerOptionGenerator
{
    public EClosedQuestionAnswerDisplayType AnswerDisplayType => EClosedQuestionAnswerDisplayType.CountryName;

    public AnswerOption GenerateAnswerOption(Country country, bool isCorrect)
    {
        return new AnswerOption
        {
            Id = country.Id,
            DisplayText = country.CommonName,
            FlagUrl = string.Empty,
            IsCorrect = isCorrect,
        };
    }
}
