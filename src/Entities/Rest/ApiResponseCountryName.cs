namespace CounTrivia.Entities.Rest;

public record class ApiResponseCountryName
{
    public string Common { get; set; } = default!;
    public string Official { get; set; } = default!;
}
