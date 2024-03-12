using AutoMapper;
using CounTrivia.Entities;
using CounTrivia.Services.AnswerGenerator;
using CounTrivia.Services.CountryProvider;

namespace CounTrivia.Services.ChallengeProvider;

public class NeighbouringCountriesChallengeProvider : AbstractChallengeProvider
{
    public override List<IAnswerOptionGenerator> AnswerOptionGenerators => new List<IAnswerOptionGenerator>
    {
        new CountryNameAnswerGenerator(),
        new SvgFlagUrlAnswerGenerator(),
    };

    public NeighbouringCountriesChallengeProvider(IMapper mapper, ICountryProvider countryProvider, IConfiguration configuration)
        : base(mapper, countryProvider, configuration)
    {
    }

    public override bool CanProvideChallenge(Country country)
    {
        return country.Neighbours.Any();
    }

    public override async Task<Challenge> GetChallenge(Country country)
    {
        List<Country> alternativeSolutions = await GetAlternativeSolutions(country);

        List<Country> correctSolutions = (List<Country>)country.Neighbours
            .Select(async neighbourCCA3 => await _countryProvider.GetCountryByCca3Async(neighbourCCA3))
            .Select(task => task.Result)
            .OfType<Country>()
            .ToList();

        return new Challenge
        {
            Task = $"Select all neighbouring countries of {country.CommonName}.",
            AnswerFormat = EAnswerFormat.MultipleChoice,
            CorrectAnswers = correctSolutions,
            AlternativeAnswers = alternativeSolutions,
            AnswerOptionGenerator = GetAnswerOptionGenerator(),
        };
    }
}
