using AutoMapper;
using CounTrivia.Entities;
using CounTrivia.Services.ChallengeProvider;
using CounTrivia.Services.CountryProvider;

namespace CounTrivia.Services.ChallengeGenerator;

public class ChallengeGenerator : IChallengeGenerator
{
    protected readonly IMapper _mapper;
    protected readonly ICountryProvider _countryProvider;
    private readonly IConfiguration _configuration;

    public ChallengeGenerator(IMapper mapper, ICountryProvider countryProvider, IConfiguration configuration)
    {
        _mapper = mapper;
        _countryProvider = countryProvider;
        _configuration = configuration;
    }

    public async Task<Challenge?> GenerateChallengeAsync()
    {
        List<Type> challengeGeneratorTypes = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => typeof(IChallengeProvider).IsAssignableFrom(type) && type.IsClass && !type.IsAbstract)
            .ToList();

        if (challengeGeneratorTypes.Count == 0)
        {
            throw new Exception("No suitable challenge generator found.");
        }

        Random randomNumberGenerator = new Random();
        Challenge? challenge = null;

        Country? country;

        do
        {
            country = await _countryProvider.GetRandomCountryAsync();

            if (country is null)
            {
                // The restcountry api seems to be unavailable?
                return null;
            }

            do
            {
                int challengeGeneratorIndex = randomNumberGenerator.Next(0, challengeGeneratorTypes.Count);
                Type challengeGeneratorType = challengeGeneratorTypes[challengeGeneratorIndex];
                IChallengeProvider challengeProvider = (IChallengeProvider)Activator.CreateInstance(challengeGeneratorType, _mapper, _countryProvider, _configuration)!;

                if (!challengeProvider.CanProvideChallenge(country))
                {
                    challengeGeneratorTypes.Remove(challengeGeneratorType);
                    continue;
                }

                challenge = await challengeProvider.GetChallenge(country);
            } while (challengeGeneratorTypes.Count > 0 && challenge is null);

            if (challenge is null)
            {
                return await GenerateChallengeAsync();
            }
        } while (challenge is null);

        return challenge;
    }
}