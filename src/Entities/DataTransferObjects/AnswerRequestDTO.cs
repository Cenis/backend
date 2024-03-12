using System.ComponentModel.DataAnnotations;

namespace CounTrivia.Entities.DataTransferObjects;

public class AnswerRequestDTO
{
    [Required]
    public Guid ChallengeId { get; set; }

    public List<Guid> SelectedAnswerOptions { get; set; } = default!;

    public string FreeTextAnswer { get; set; } = default!;
}