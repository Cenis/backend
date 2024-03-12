using System.Text.Json.Serialization;

namespace CounTrivia.Entities.DataTransferObjects;

public class AnswerOptionResponseDTO
{
    public Guid Id { get; set; }

    [JsonIgnore]
    public bool IsCorrect { get; set; }

    public string DisplayText { get; set; } = default!;

    public string FlagUrl { get; set; } = default!;
}