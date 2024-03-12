using CounTrivia.Entities;
using CounTrivia.Entities.DataTransferObjects;

namespace CounTrivia.Services.AnswerEvaluator;

public class FreeTextAnswerEvaluator : IAnswerEvaluator
{
    private Challenge _challenge;

    public Challenge Challenge { get => _challenge; init => _challenge = value; }

    public FreeTextAnswerEvaluator(Challenge challenge)
    {
        _challenge = challenge;
    }

    public float Evaluate(AnswerRequestDTO answerRequest)
    {
        List<string> correctAnswers = Challenge.CorrectAnswers.Select(country => country.Capital?.ToLower())
                                                              .OfType<string>()
                                                              .ToList<string>();

        if (correctAnswers.Contains(answerRequest.FreeTextAnswer.ToLower()))
        {
            return 1;
        }

        return 0;
    }
}