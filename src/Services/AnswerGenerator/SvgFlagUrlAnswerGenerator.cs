using CounTrivia.Entities;

namespace CounTrivia.Services.AnswerGenerator;

public class SvgFlagUrlAnswerGenerator : IAnswerOptionGenerator
{
    public EClosedQuestionAnswerDisplayType AnswerDisplayType => EClosedQuestionAnswerDisplayType.FlagSvg;

    public AnswerOption GenerateAnswerOption(Country country, bool isCorrect)
    {
        return new AnswerOption
        {
            Id = country.Id,
            DisplayText = country.CommonName,
            FlagUrl = country.Flags.SvgUrl,
            IsCorrect = isCorrect,
        };
    }
}
