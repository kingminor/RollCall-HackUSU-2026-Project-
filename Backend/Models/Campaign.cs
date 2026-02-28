namespace Backend.Models;
using Backend.Models.Enums;

public class Campaign
{
    public string id { get; set; } = Guid.NewGuid().ToString();
    public string? AlphaNumericJoinCode { get; set; }
    public PlayerUser DM { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public ushort MaxPlayers { get; set; }
    public string? Location  { get; set; }
    public string? Setting { get; set; }
    public DateTime LastUpdated { get; set; } = DateTime.Now;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public ICollection<CampaignMembership>?  CampaignMemberships { get; set; }
    
    //ENUMS
    public ExperienceLevel? ExperienceLevel { get; set; }
    public CampaignLocationType? CampaignLocationType { get; set; }
    public CampaignStatus? Status { get; set; }
    public PreferredPlaystyle? PreferredPlaystyle { get; set; }
    public CampaignTone? CampaignTone { get; set; }
    public SessionFrequency? SessionFrequency { get; set; }
    public StatSystem? StatSystem { get; set; }
    public ContentMaturity? ContentMaturity { get; set; }
}