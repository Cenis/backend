namespace CounTrivia.Entities.DataTransferObjects;

public class AnswerResponseDTO
{
    public int MaxPoints { get; set; }
    
    public int AwardedPoints { get; set; }
    
    public List<Guid> CorrectAnswerOptions { get; set; } = default!;
    
    public List<string> CorrectAnswerValues { get; set; } = default!;
}