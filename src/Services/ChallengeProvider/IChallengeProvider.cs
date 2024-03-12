using CounTrivia.Entities;
using CounTrivia.Services.AnswerGenerator;

namespace CounTrivia.Services.ChallengeProvider;

public interface IChallengeProvider
{
    public List<IAnswerOptionGenerator> AnswerOptionGenerators { get; }

    public bool CanProvideChallenge(Country country);

    public Task<Challenge> GetChallenge(Country country);
}
