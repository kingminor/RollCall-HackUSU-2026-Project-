namespace Backend.Models;

public class CampaignMembership
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public Campaign Campaign { get; set; }
    public PlayerUser PlayerUser { get; set; }
    public Character ActiveCharacterId { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.Now;
    public DateTime LastUpdated { get; set; }
    public bool IsApproved { get; set; }
}