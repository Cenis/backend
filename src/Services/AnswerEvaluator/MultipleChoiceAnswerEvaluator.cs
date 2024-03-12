using CounTrivia.Entities;
using CounTrivia.Entities.DataTransferObjects;

namespace CounTrivia.Services.AnswerEvaluator;

public class MultipleChoiceAnswerEvaluator : IAnswerEvaluator
{
    private Challenge _challenge;

    public Challenge Challenge { get => _challenge; init => _challenge = value; }

    public MultipleChoiceAnswerEvaluator(Challenge challenge)
    {
        _challenge = challenge;
    }

    public float Evaluate(AnswerRequestDTO answerRequest)
    {
        List<Guid> correctAnswerGuids = Challenge.CorrectAnswers.Select(country => country.Id).ToList();

        int deductions = 0;

        // false positive answers
        foreach (Guid submittedAnswerGuid in answerRequest.SelectedAnswerOptions)
        {
            if (!correctAnswerGuids.Contains(submittedAnswerGuid))
            {
                deductions++;
            }
        }

        // false negative answers
        foreach (Guid correctAnswerGuid in correctAnswerGuids)
        {
            if (!answerRequest.SelectedAnswerOptions.Contains(correctAnswerGuid))
            {
                deductions++;
            }
        }

        int totalAnswersOptions = Challenge.CorrectAnswers.Count + Challenge.AlternativeAnswers.Count;
        float relativeDeduction = (float)deductions / totalAnswersOptions;

        return 1 - relativeDeduction;
    }
}