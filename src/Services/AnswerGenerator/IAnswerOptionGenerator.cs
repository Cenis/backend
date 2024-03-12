using CounTrivia.Entities;

namespace CounTrivia.Services.AnswerGenerator;

public interface IAnswerOptionGenerator
{
    public EClosedQuestionAnswerDisplayType AnswerDisplayType { get; }
    public AnswerOption GenerateAnswerOption(Country country, bool isCorrect);
}