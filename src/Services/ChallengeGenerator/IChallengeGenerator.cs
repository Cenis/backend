using CounTrivia.Entities;

namespace CounTrivia.Services.ChallengeGenerator;

public interface IChallengeGenerator
{
    public Task<Challenge?> GenerateChallengeAsync();
}
