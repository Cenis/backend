using AutoMapper;
using CounTrivia.Entities;
using CounTrivia.Services.AnswerGenerator;
using CounTrivia.Services.CountryProvider;

namespace CounTrivia.Services.ChallengeProvider;

public class RecognizeDescribedFlagChallengeProvider : AbstractChallengeProvider
{
    public override List<IAnswerOptionGenerator> AnswerOptionGenerators => new List<IAnswerOptionGenerator>
    {
        new CountryNameAnswerGenerator(),
        new SvgFlagUrlAnswerGenerator(),
    };

    public RecognizeDescribedFlagChallengeProvider(IMapper mapper, ICountryProvider countryProvider, IConfiguration configuration)
        : base(mapper, countryProvider, configuration)
    {
    }

    public override bool CanProvideChallenge(Country country)
    {
        return !string.IsNullOrEmpty(country.Flags.Description);
    }

    public override async Task<Challenge> GetChallenge(Country country)
    {
        List<Country> alternativeSolutions = await GetAlternativeSolutions(country);

        return new Challenge
        {
            Task = "Which country is this: " + country.Flags.Description!.Replace(country.OfficialName, "***")
                                                                         .Replace(country.CommonName, "***"),
            AnswerFormat = EAnswerFormat.SingleChoice,
            CorrectAnswers = new List<Country> { country },
            AlternativeAnswers = alternativeSolutions,
            AnswerOptionGenerator = GetAnswerOptionGenerator(),
        };
    }
}