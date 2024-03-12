using AutoMapper;
using CounTrivia.Entities;
using CounTrivia.Services.AnswerGenerator;
using CounTrivia.Services.CountryProvider;
 
namespace CounTrivia.Services.ChallengeProvider;
 
public abstract class AbstractChallengeProvider : IChallengeProvider
{
    protected readonly IMapper _mapper;
    protected readonly ICountryProvider _countryProvider;
    private readonly IConfiguration _configuration;
    private readonly Random _randomNumberGenerator;
 
    public abstract List<IAnswerOptionGenerator> AnswerOptionGenerators { get; }
 
    public AbstractChallengeProvider(IMapper mapper, ICountryProvider countryProvider, IConfiguration configuration)
    {
        _mapper = mapper;
        _countryProvider = countryProvider;
        _configuration = configuration;
        _randomNumberGenerator = new Random();
    }
 
    public abstract bool CanProvideChallenge(Country country);
 
    public abstract Task<Challenge> GetChallenge(Country country);
 
    protected async Task<List<Country>> GetAlternativeSolutions(Country country)
    {
        List<Country>? countries = await _countryProvider.GetAllCountriesAsync();
 
        if (countries is null || countries.Count == 0)
        {
            return new List<Country>();
        }
 
        List<Country> alternativeAnswerCountries = new List<Country>();
 
        // TODO: Make alternative answer count variance configurable
        int alternativeAnswerCount = int.Parse(Environment.GetEnvironmentVariable("alternativeAnswerCount") ?? "2");
        int alternativeAnswerCountVariance = int.Parse(Environment.GetEnvironmentVariable("alternativeAnswerCountVariance") ?? "5");
 
        // If alternativ answer count variance is > 0 and valid...
        if (alternativeAnswerCountVariance > 0 && alternativeAnswerCountVariance <= alternativeAnswerCount)
        {
            // ... randomly determine count of alternative answers.
            alternativeAnswerCount = _randomNumberGenerator.Next(alternativeAnswerCount - alternativeAnswerCountVariance, alternativeAnswerCount + alternativeAnswerCountVariance + 1);
        }
 
        do
        {
            int countryIndex = _randomNumberGenerator.Next(0, countries.Count);
            Country answerCandidate = countries[countryIndex];
            if (answerCandidate.CCA3 != country.CCA3 && !alternativeAnswerCountries.Contains(answerCandidate))
            {
                alternativeAnswerCountries.Add(answerCandidate);
            }
        } while (alternativeAnswerCountries.Count < alternativeAnswerCount);
 
        return alternativeAnswerCountries;
    }
 
    protected IAnswerOptionGenerator GetAnswerOptionGenerator()
    {
        int index = _randomNumberGenerator.Next(0, AnswerOptionGenerators.Count);
        return AnswerOptionGenerators[index];
    }
}
 