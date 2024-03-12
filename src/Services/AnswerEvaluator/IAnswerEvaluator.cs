using CounTrivia.Entities;
using CounTrivia.Entities.DataTransferObjects;

namespace CounTrivia.Services.AnswerEvaluator;

public interface IAnswerEvaluator
{
    public Challenge Challenge { get; init; }
    public float Evaluate(AnswerRequestDTO answerRequest);
}