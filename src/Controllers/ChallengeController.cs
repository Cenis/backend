using AutoMapper;
using CounTrivia.Entities;
using CounTrivia.Entities.DataTransferObjects;
using CounTrivia.Extensions;
using CounTrivia.Services;
using CounTrivia.Services.ChallengeGenerator;
using CounTrivia.Services.CountryProvider;
using Microsoft.AspNetCore.Mvc;

namespace CounTrivia.Controllers;

[ApiController]
[Route("[controller]")]
public class ChallengeController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly CounTriviaDbContext _dbContext;
    private readonly ICountryProvider _countryProvider;
    private readonly IChallengeGenerator _challengeGenerator;

    public ChallengeController(
        IMapper mapper,
        CounTriviaDbContext dbContext,
        ICountryProvider countryProvider,
        IChallengeGenerator challengeGenerator
    )
    {
        _mapper = mapper;
        _dbContext = dbContext;
        _countryProvider = countryProvider;
        _challengeGenerator = challengeGenerator;
    }

    [HttpGet]
    public async Task<ActionResult<ChallengeResponseDTO>> GetChallenge()
    {
        Challenge? challenge = await _challengeGenerator.GenerateChallengeAsync();

        if (challenge is null)
        {
            return NoContent();
        }

        await _dbContext.Challenges.AddAsync(challenge);
        await _dbContext.SaveChangesAsync();

        ChallengeResponseDTO responseDTO = _mapper.Map<ChallengeResponseDTO>(challenge);
        responseDTO.AnswerOptions.Shuffle<AnswerOptionResponseDTO>();

        if (Enum.Parse<EAnswerFormat>(responseDTO.AnswerFormat) == EAnswerFormat.FreeText)
        {
            responseDTO.AnswerOptions = new List<AnswerOptionResponseDTO>();
        }

        return responseDTO;
    }

}
