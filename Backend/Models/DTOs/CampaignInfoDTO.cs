namespace Backend.Models.DTOs;

public class CampaignInfoDTO
{
    public string Name { get; set; }
    public string Description { get; set; }
    public ushort MaxPlayers { get; set; }
    public bool IsPublic { get; set; }
    public string? Location { get; set; }
    public string setting { get; set; }
}