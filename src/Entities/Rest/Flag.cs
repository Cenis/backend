namespace CounTrivia.Entities.Rest;

public record class ApiResponseFlagInformation
{
    public string Svg { get; set; } = default!;
    public string Png { get; set; } = default!;
    public string Alt { get; set; } = default!;
}