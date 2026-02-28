namespace Backend.Models ;
using Backend.Models.Enums;

public class Character
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string PlayerId { get; set; }
    public PlayerUser Player { get; set; }
    public string? CampaignId { get; set; }
    public Campaign? Campaign { get; set; } = null;
    public DateTime DateCreated { get; set; } = DateTime.Now;
    public DateTime LastUpdated { get; set; } = DateTime.Now;
    
    //Info Strings
    public string? Name { get; set; }
    public string? Class { get; set; }
    public string? Race { get; set; }
    public string? Background { get; set; }
    public string? Alignment { get; set; }
    public string? PersonalityTraits { get; set; }
    public string? Ideals  { get; set; }
    public string? Bonds  { get; set; }
    public string? Flaws  { get; set; }
    public string? Backstory { get; set; }
}