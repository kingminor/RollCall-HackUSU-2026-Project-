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
}