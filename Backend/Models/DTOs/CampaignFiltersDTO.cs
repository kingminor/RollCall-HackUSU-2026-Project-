namespace Backend.Models.DTOs;
using Backend.Models.Enums;

public class CampaignFiltersDTO
{
    public ExperienceLevel? ExperienceLevel { get; set; }
    public CampaignLocationType? CampaignLocationType { get; set; }
    public CampaignStatus? Status { get; set; }
    public PreferredPlaystyle? PreferredPlaystyle { get; set; }
    public CampaignTone? CampaignTone { get; set; }
    public SessionFrequency? SessionFrequency { get; set; }
    public StatSystem? StatSystem { get; set; }
    public ContentMaturity? ContentMaturity { get; set; }
}