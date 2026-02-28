namespace Backend.Models.DTOs;
using Backend.Models.Enums;

public class CampaignFiltersDTO
{
    public ExperienceLevel? experienceLevel { get; set; }
    public CampaignLocationType? campaignLoacationType { get; set; }
    public CampaignStatus? status { get; set; }
    public PreferredPlaystyle? preferredPlaystyle { get; set; }
    public CampaignTone? campaignTone { get; set; }
    public SessionFrequency? sessionFrequency { get; set; }
    public StatSystem? statSystem { get; set; }
    ContentMaturity? contentMaturity { get; set; }
}