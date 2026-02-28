namespace Backend.Models.DTOs;

public class CreateCampaignRequest
{
    public CampaignInfoDTO CampaignInfo { get; set; }
    public CampaignFiltersDTO CampaignFilters { get; set; }
}