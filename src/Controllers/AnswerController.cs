using AutoMapper;
using CounTrivia.Entities;
using CounTrivia.Entities.DataTransferObjects;
using CounTrivia.Services;
using CounTrivia.Services.AnswerEvaluator;
using CounTrivia.Services.ChallengeGenerator;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("[controller]")]
public class AnswerController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly CounTriviaDbContext _dbContext;
    private readonly IChallengeGenerator _challengeGenerator;

    public AnswerController(
            IMapper mapper,
            CounTriviaDbContext dbContext,
            IChallengeGenerator challengeGenerator
        )
    {
        _mapper = mapper;
        _dbContext = dbContext;
        _challengeGenerator = challengeGenerator;
    }

    [HttpPost]
    public async Task<ActionResult<AnswerResponseDTO>> PostAnswer(AnswerRequestDTO answerRequest)
    {
        Challenge? challenge = await _dbContext.Challenges.Where(challenge => challenge.Id == answerRequest.ChallengeId)
                                                          .Include(challenge => challenge.CorrectAnswers)
                                                          .Include(challenge => challenge.AlternativeAnswers)
                                                          .FirstOrDefaultAsync();

        if (challenge is null)
        {
            return NotFound();
        }

        IAnswerEvaluator answerEvaluator = AnswerEvaluatorFactory.GetAnswerEvaluator(challenge);
        float shareOfPoints = answerEvaluator.Evaluate(answerRequest);

        return new AnswerResponseDTO
        {
            MaxPoints = challenge.MaxPoints,
            AwardedPoints = (int)Math.Round(challenge.MaxPoints * shareOfPoints),
            CorrectAnswerOptions = challenge.CorrectAnswers.Select(answer => answer.Id).ToList(),
            CorrectAnswerValues = challenge.CorrectAnswers.Select(answer => answer.Capital)
                                                          .OfType<string>()
                                                          .ToList(),
        };
    }
}
