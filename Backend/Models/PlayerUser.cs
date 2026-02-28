using Microsoft.AspNetCore.Identity;

namespace Backend.Models;

public class PlayerUser : IdentityUser
{
    public string? AboutMe { get; set; }
    public List<CampaignMembership> CampaignMemberships { get; set; } = new List<CampaignMembership>();
    public List<string> CharacterIds { get; set; } = new List<string>();
    public DateTime LastUpdated { get; set; } = DateTime.Now;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
}