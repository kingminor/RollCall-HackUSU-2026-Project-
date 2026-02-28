namespace Backend.Models.DTOs;

public class StatDTO
{
    public string Id { get; set; } = new Guid().ToString();
    public byte STRStat { get; set; }
    public byte DEXStat { get; set; }
    public byte CONStat { get; set; }
    public byte INTStat { get; set; }
    public byte WISStat { get; set; }
    public byte CHAStat { get; set; }
}