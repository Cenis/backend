namespace CounTrivia.Entities.DataTransferObjects;

public class ChallengeResponseDTO
{
    public Guid Id { get; init; }

    public string Task { get; init; } = default!;
    
    public int MaxPoints { get; set; }

    public string AnswerFormat { get; init; } = default!;

    public string AnswerDisplayType { get; init; } = default!;

    public List<AnswerOptionResponseDTO> AnswerOptions { get; set; } = default!;
}