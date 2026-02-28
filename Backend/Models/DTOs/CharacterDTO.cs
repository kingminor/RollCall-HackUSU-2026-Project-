using Backend.Models.Enums;

namespace Backend.Models.DTOs;

public class CharacterDTO
{
    public string? Name { get; set; }
    public string? Class { get; set; }
    public string? Race { get; set; }
    public string? Background { get; set; }
    public Alignment? Alignment { get; set; }
    public string? PersonalityTraits { get; set; }
    public string? Ideals  { get; set; }
    public string? Bonds  { get; set; }
    public string? Flaws  { get; set; }
    public string? Backstory { get; set; }
}