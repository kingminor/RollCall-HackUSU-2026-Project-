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
    private readonly SignInManager<PlayerUser> _signInManager;
    private readonly ApplicationDbContext _dbContext;

    public CampaignController(UserManager<PlayerUser> userManager, ApplicationDbContext context, SignInManager<PlayerUser> signInManager)
    {
        _userManager = userManager;
        _dbContext = context;
        _signInManager = signInManager;
    }
    
    [HttpPost("create")]
    public async Task<IActionResult> Create(CreateCampaignRequest request)
    {
        var user = await _userManager.GetUserAsync(User);
        
        if(user == null) return Unauthorized();

        var campaign = new Campaign
        {
            DM = user,
            Name = request.CampaignInfo.Name,
            Description = request.CampaignInfo.Description,
            MaxPlayers = request.CampaignInfo.MaxPlayers,
            Location = request.CampaignInfo.Location,
            Setting = request.CampaignInfo.Setting,
            IsPublic = request.CampaignInfo.IsPublic,

            ExperienceLevel = request.CampaignFilters.ExperienceLevel,
            CampaignLocationType = request.CampaignFilters.CampaignLocationType,
            Status = request.CampaignFilters.Status,
            PreferredPlaystyle = request.CampaignFilters.PreferredPlaystyle,
            CampaignTone = request.CampaignFilters.CampaignTone,
            SessionFrequency = request.CampaignFilters.SessionFrequency,
            StatSystem = request.CampaignFilters.StatSystem,
            ContentMaturity = request.CampaignFilters.ContentMaturity
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
            .FirstOrDefaultAsync(c => c.AlphaNumericJoinCode == alphacode);
        
        if (campaign == null) return NotFound();
        return Ok(campaign);
    }

    [HttpGet("getMyCampaigns")]
    public async Task<IActionResult> GetMyCampaigns()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return Unauthorized();

        var campaigns = await _dbContext.Campaigns
            .Where(c => c.CampaignMemberships.Any(m => m.PlayerUser.Id == user.Id))
            .Select(c => new
            {
                Id = c.id,
                Name = c.Name,
                Description = c.Description,
                IsPublic = c.IsPublic,
                DM = new UserDTO
                {
                    Id = c.DMId,
                    UserName = c.DM.UserName
                },
                CampaignMemberships = c.CampaignMemberships.Count
            }).ToListAsync();

        return Ok(campaigns);
    }

    [HttpGet("getPublicCampaigns")]
    public async Task<IActionResult> GetPublicCampaigns()
    {
        var campaigns = await _dbContext.Campaigns
            .Where(m => m.IsPublic)
            .Include(c => c.DM)
            .Include(c => c.CampaignMemberships)
            .Select(c => new
            {
                Id = c.id,
                Name = c.Name,
                Description = c.Description,
                IsPublic = c.IsPublic,
                DM = new UserDTO
                {
                    Id = c.DMId,
                    UserName = c.DM.UserName,
                },
                CampaignMemberships = c.CampaignMemberships.Count
            })
            .ToListAsync();

        return Ok(campaigns);
    }

    [HttpPost("requestJoinCampaignId")]
    public async Task<IActionResult> RequestJoinCampaignId(string id)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return Unauthorized();
        
        var campaign = await _dbContext.Campaigns
            .FindAsync(id);
        if (campaign == null) return NotFound();

        try
        {
            var campaignMembership = new CampaignMembership
            {
                Campaign = campaign,
                PlayerUser = user,
            };

            campaign.CampaignMemberships.Add(campaignMembership);

            await _dbContext.SaveChangesAsync();
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("requestJoinCampaignAlphaCode")]
    public async Task<IActionResult> RequestJoinCampaignAlphaCode(string code)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return Unauthorized();
        
        var campaign = await _dbContext.Campaigns
            .FirstOrDefaultAsync(c => c.AlphaNumericJoinCode == code);
        if (campaign == null) return NotFound();

        try
        {
            var campaignMembership = new CampaignMembership
            {
                Campaign = campaign,
                PlayerUser = user,
            };

            campaign.CampaignMemberships.Add(campaignMembership);

            await _dbContext.SaveChangesAsync();
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("approveJoinRequest")]
    public async Task<IActionResult> ApproveJoinRequest(string campaignMembershipId)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return Unauthorized();
        
        var campaignMembership = await _dbContext.CampaignMemberships
            .FindAsync(campaignMembershipId);
        
        if (campaignMembership == null) return NotFound();
        
        var campaign = await _dbContext.Campaigns
            .FindAsync(campaignMembership.CampaignId);
        
        if (campaign == null) return NotFound();
        if(campaign.DMId != user.Id) return Unauthorized();
        
        campaignMembership.IsApproved = true;
        await _dbContext.SaveChangesAsync();
        return Ok();
    }
    
    [HttpPost("denyJoinRequest")]
    public async Task<IActionResult> DenyJoinRequest(string campaignMembershipId)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return Unauthorized();
        
        var campaignMembership = await _dbContext.CampaignMemberships
            .FindAsync(campaignMembershipId);
        
        if (campaignMembership == null) return NotFound();
        
        var campaign = await _dbContext.Campaigns
            .FindAsync(campaignMembership.CampaignId);
        
        if (campaign == null) return NotFound();
        if(campaign.DMId != user.Id) return Unauthorized();
        
        campaignMembership.IsApproved = false;
        await _dbContext.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete("leaveCampaign")]
    public async Task<IActionResult> LeaveCampaign(string campaignId)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return Unauthorized();
        
        var membership = await _dbContext.CampaignMemberships
            .FirstOrDefaultAsync(m =>
                m.CampaignId == campaignId &&
                m.PlayerUserId == user.Id);
        
        if (membership == null) return NotFound();

        try
        {
            _dbContext.CampaignMemberships.Remove(membership);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
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