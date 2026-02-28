using Backend.Models;

namespace Backend;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : IdentityDbContext<PlayerUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    
    public DbSet<Campaign> Campaigns { get; set; }
    public DbSet<CampaignMembership>  CampaignMemberships { get; set; }
    public DbSet<Character> Characters { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Campaign>()
            .HasIndex(u => u.AlphaNumericJoinCode)
            .IsUnique();
        
        modelBuilder.Entity<Campaign>()
            .HasIndex(u => u.id)
            .IsUnique();
        
        modelBuilder.Entity<CampaignMembership>()
            .HasIndex(m => new { m.CampaignId, m.PlayerUserId })
            .IsUnique();

    }
}