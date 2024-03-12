using CounTrivia.Entities;
using CounTrivia.Entities.DataTransferObjects;

namespace CounTrivia.Services.AnswerEvaluator;

public class SingleChoiceAnswerEvaluator : IAnswerEvaluator
{
    private Challenge _challenge;

    public Challenge Challenge { get => _challenge; init => _challenge = value; }

    public SingleChoiceAnswerEvaluator(Challenge challenge)
    {
        _challenge = challenge;
    }
    
    public float Evaluate(AnswerRequestDTO answerRequest)
    {
        if (answerRequest.SelectedAnswerOptions.Count != 1)
        {
            return 0;
        }

        Guid submittedAnswerGuid = answerRequest.SelectedAnswerOptions[0];
        if (Challenge.CorrectAnswers.Any(correctAnswer => correctAnswer.Id.Equals(submittedAnswerGuid)))
        {
            return 1;
        }

        return 0;
    }
}