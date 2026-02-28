namespace Backend.Models.DTOs;

public class CampaignDTO
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int MaxPlayers { get; set; }
    public bool IsPublic { get; set; }
    public string AlphaNumericCode { get; set; }
    public string? Location  { get; set; }
    public string? Setting { get; set; }
    
    public UserDTO DM { get; set; }
}