using AutoMapper;
using CounTrivia.Entities;
using CounTrivia.Services.AnswerGenerator;
using CounTrivia.Services.CountryProvider;

namespace CounTrivia.Services.ChallengeProvider;

public class CapitalChallengeProvider : AbstractChallengeProvider
{
    public override List<IAnswerOptionGenerator> AnswerOptionGenerators => new List<IAnswerOptionGenerator>
    {
        new FreeTextAnswerGenerator(),
    };

    public CapitalChallengeProvider(IMapper mapper, ICountryProvider countryProvider, IConfiguration configuration)
        : base(mapper, countryProvider, configuration)
    {
    }

    public override bool CanProvideChallenge(Country country)
    {
        return !string.IsNullOrEmpty(country.Capital);
    }

    public override async Task<Challenge> GetChallenge(Country country)
    {
        Challenge challenge = new Challenge
        {
            Task = $"Name the capital of {country.CommonName}.",
            AnswerFormat = EAnswerFormat.FreeText,
            CorrectAnswers = new List<Country> { country },
            AlternativeAnswers = new List<Country>(),
            AnswerOptionGenerator = GetAnswerOptionGenerator(),
        };

        return await Task.FromResult<Challenge>(challenge);
    }
}