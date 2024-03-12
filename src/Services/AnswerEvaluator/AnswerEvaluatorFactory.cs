using CounTrivia.Entities;

namespace CounTrivia.Services.AnswerEvaluator;

public class AnswerEvaluatorFactory
{
    public static IAnswerEvaluator GetAnswerEvaluator(Challenge challenge)
    {
        return challenge.AnswerFormat switch
        {
            EAnswerFormat.MultipleChoice => new MultipleChoiceAnswerEvaluator(challenge),
            EAnswerFormat.SingleChoice => new SingleChoiceAnswerEvaluator(challenge),
            EAnswerFormat.FreeText => new FreeTextAnswerEvaluator(challenge),
            _ => throw new NotSupportedException($"There's no answer evaluator configured for answer format type '{challenge.AnswerFormat.ToString()}'.")
        };
    }
}