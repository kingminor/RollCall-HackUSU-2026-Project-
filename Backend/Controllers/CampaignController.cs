using Backend.Models;
using Backend.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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
        
        return Ok(campaign);
    }
}