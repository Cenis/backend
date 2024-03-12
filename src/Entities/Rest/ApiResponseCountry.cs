using System.Text.Json.Serialization;

namespace CounTrivia.Entities.Rest;

public record class ApiResponseCountry
{
    // common name
    public ApiResponseCountryName Name { get; set; } = default!;

    // code ISO 3166-1 alpha-2
    public string CCA2 { get; set; } = default!;

    // code ISO 3166-1 alpha-3
    public string CCA3 { get; set; } = default!;

    // code International Olympic Committee
    public string Cioc { get; set; } = default!;

    // whether the country is member of the united nations
    [JsonPropertyName("unMember")]
    public bool IsUnMember { get; set; }

    // capital of the country
    public List<string> Capital { get; set; } = default!;

    // whether the country is not connected to an ocean
    [JsonPropertyName("landlocked")]
    public bool IsLandlocked { get; set; }

    // cca3 codes of countries with common border
    public string[] Borders { get; set; } = default!;

    // area in square kilometers
    public float Area { get; set; }

    // population count
    public long Population { get; set; }

    // timezones
    public string[] Timezones { get; set; } = default!;

    // flag emoji
    public string Flag { get; set; } = default!;

    // additional flag information
    public ApiResponseFlagInformation Flags { get; set; } = default!;
}