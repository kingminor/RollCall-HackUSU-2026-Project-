using Backend.Models;
using Backend.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers;

[Authorize]
[ApiController]
[Route("api/campaign")]
public class CampaignController : ControllerBase
{
    private readonly UserManager<PlayerUser> _userManager;
    private readonly ApplicationDbContext _dbContext;

    public CampaignController(UserManager<PlayerUser> userManager, ApplicationDbContext context)
    {
        _userManager = userManager;
        _dbContext = context;
    }
    
    [HttpPost("create")]
    public async Task<IActionResult> Create(CampaignInfoDTO campaignInfo, CampaignFiltersDTO campaignFilters)
    {
        var user = await _userManager.GetUserAsync(User);
        
        if(user == null) return Unauthorized();

        var campaign = new Campaign
        {
            DM = user,
            Name = campaignInfo.Name,
            Description = campaignInfo.Description,
            MaxPlayers = campaignInfo.MaxPlayers,
            Location = campaignInfo.Location,
            Setting = campaignInfo.Setting,
            ExperienceLevel = campaignFilters.ExperienceLevel,
            CampaignLocationType = campaignFilters.CampaignLocationType,
            Status = campaignFilters.Status,
            PreferredPlaystyle = campaignFilters.PreferredPlaystyle,
            CampaignTone = campaignFilters.CampaignTone,
            SessionFrequency = campaignFilters.SessionFrequency,
            StatSystem = campaignFilters.StatSystem,
            ContentMaturity = campaignFilters.ContentMaturity
        };

        var campaignMembership = new CampaignMembership
        {
            Campaign = campaign,
            PlayerUser = user,
            ActiveCharacter = null,
            IsApproved = true
        };
        
        campaign.CampaignMemberships.Add(campaignMembership);
        user.CampaignMemberships.Add(campaignMembership);
        
        while (true)
        {
            campaign.AlphaNumericJoinCode = GenerateJoinCode();

            _dbContext.Campaigns.Add(campaign);

            try
            {
                await _dbContext.SaveChangesAsync();
                break;
            }
            catch (DbUpdateException)
            {
                // Collision happened
                _dbContext.Entry(campaign).State = EntityState.Detached;
            }
        }
        
        return Ok("Lol, this returns something so you have to deal with it now!");
    }

    [HttpGet("getById")]
    public async Task<IActionResult> GetById(string id)
    {
        var campaign = await _dbContext.Campaigns
            .FindAsync(id);
        
        if (campaign == null) return NotFound();
        return Ok(campaign);
    }

    [HttpGet("getByAlphaCode")]
    public async Task<IActionResult> GetByAlphaCode(string alphacode)
    {
        var campaign = await _dbContext.Campaigns
            .FindAsync(alphacode);
        
        if (campaign == null) return NotFound();
        return Ok(campaign);
    }

    [HttpGet("getMyCampaigns")]
    public async Task<IActionResult> GetMyCampaigns()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return Unauthorized();

        var campaigns = await _dbContext.Campaigns
            .Where(c => c.CampaignMemberships
                .Any(m => m.PlayerUser.Id == user.Id))
            .ToListAsync();
        
        if (!campaigns.Any())
            return NotFound();

        return Ok(campaigns);
    }
    
    private static readonly Random _random = new();

    private string GenerateJoinCode()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        return new string(Enumerable.Repeat(chars, 10)
            .Select(s => s[_random.Next(s.Length)])
            .ToArray());
    }
}