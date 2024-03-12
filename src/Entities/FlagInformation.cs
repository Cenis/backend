namespace CounTrivia.Entities;

public record class FlagInformation
{
    public string PngUrl { get; set; } = default!;
    public string SvgUrl { get; set; } = default!;
    public string Emoji { get; set; } = default!;
    public string? Description { get; set; } = default!;
}