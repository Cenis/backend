using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CounTrivia.Entities;

public record class Country
{
    public Guid Id { get; set; }
    public string CommonName { get; set; } = default!;

    public string OfficialName { get; set; } = default!;

    [NotMapped]
    public string CCA2 { get; set; } = default!;

    public string CCA3 { get; set; } = default!;

    [NotMapped]
    public string Cioc { get; set; } = default!;

    [NotMapped]
    public bool IsUnMember { get; set; }

    public string? Capital { get; set; } = default!;

    [NotMapped]
    public bool IsLandlocked { get; set; }

    [NotMapped]
    public string[] Neighbours { get; set; } = default!;

    public float Area { get; set; }

    public long Population { get; set; }

    [NotMapped]
    public string[] Timezones { get; set; } = default!;

    [NotMapped]
    public FlagInformation Flags { get; set; } = default!;
}