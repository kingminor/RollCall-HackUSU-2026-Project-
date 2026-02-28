using Backend.Models;
using Backend.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers;

[Authorize]
[ApiController]
[Route("api/character")]
public class CharacterController : ControllerBase
{
    private readonly UserManager<PlayerUser> _userManager;
    private readonly ApplicationDbContext _dbContext;
    
    public CharacterController(UserManager<PlayerUser> userManager, ApplicationDbContext dbContext)
    {
        _userManager = userManager;
        _dbContext = dbContext;
    }

    [HttpGet("getMyCharacters")]
    public async Task<IActionResult> GetCharacters()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return Unauthorized();

        var characters = await _dbContext.Characters
            .Where(m => m.PlayerId == user.Id)
            .ToListAsync();
        
        return Ok(characters);
    }

    [HttpPost("createCharacter/{campaignId}")]
    public async Task<IActionResult> CreateCharacter([FromBody] CharacterDTO characterDto, string campaignId)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            return Unauthorized();

        // Verify user is part of the campaign
        var membership = await _dbContext.CampaignMemberships
            .FirstOrDefaultAsync(c =>
                c.PlayerUserId == user.Id &&
                c.CampaignId == campaignId);

        if (membership == null)
            return NotFound("User is not a member of this campaign.");

        var newCharacter = new Character
        {
            Name = characterDto.Name,
            Class = characterDto.Class,
            Race = characterDto.Race,
            Background = characterDto.Background,
            Alignment = characterDto.Alignment,
            PersonalityTraits = characterDto.PersonalityTraits,
            Ideals = characterDto.Ideals,
            Bonds = characterDto.Bonds,
            Flaws = characterDto.Flaws,
            Backstory = characterDto.Backstory,
            STRStat = characterDto.Stats.STRStat,
            DEXStat = characterDto.Stats.DEXStat,
            CONStat = characterDto.Stats.CONStat,
            INTStat = characterDto.Stats.INTStat,
            WISStat = characterDto.Stats.WISStat,
            CHAStat = characterDto.Stats.CHAStat,
            Player = user
        };

        try
        {
            // Add character
            user.Characters.Add(newCharacter);

            // Link character to campaign
            membership.ActiveCharacter = newCharacter;

            await _dbContext.SaveChangesAsync();

            return Ok(new { characterId = newCharacter.Id });
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("getCharacterByCampaignId")]
    public async Task<IActionResult> GetCharacterByCampaignId(string campaignId)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return Unauthorized();

        var characters = await _dbContext.Characters
            .Where(m => m.CampaignId == campaignId)
            .ToListAsync();
        
        return Ok(characters);
    }
    
    [HttpGet("memberships/{campaignId}")]
    public async Task<IActionResult> GetCampaignMembership(string campaignId)
    {
        if (string.IsNullOrEmpty(campaignId))
            return BadRequest();

        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            return Unauthorized();

        var campaign = await _dbContext.Campaigns.FindAsync(campaignId);
        if (campaign == null)
            return NotFound();

        if (campaign.DMId == user.Id)
        {
            var memberships = await _dbContext.CampaignMemberships
                .Where(m => m.CampaignId == campaignId)
                .Select(m => new
                {
                    CampaignName = m.Campaign.Name,
                    CampaignDescription = m.Campaign.Description,
                    CampaignCode = m.Campaign.AlphaNumericJoinCode,
                    Id = m.Id,
                    DM = campaign.DM.UserName,
                    PlayerUserId = m.PlayerUserId,
                    PlayerName = m.PlayerUser.UserName,
                    ActiveCharacter = m.ActiveCharacter != null 
                        ? new CharacterDTO
                        {
                            Name = m.ActiveCharacter.Name,
                            Class = m.ActiveCharacter.Class,
                            Race = m.ActiveCharacter.Race,
                            Background = m.ActiveCharacter.Background,
                            Alignment = m.ActiveCharacter.Alignment,
                            PersonalityTraits = m.ActiveCharacter.PersonalityTraits,
                            Ideals = m.ActiveCharacter.Ideals,
                            Bonds = m.ActiveCharacter.Bonds,
                            Flaws = m.ActiveCharacter.Flaws,
                            Backstory = m.ActiveCharacter.Backstory,
                            Stats = new StatDTO
                            {
                                STRStat = (byte)m.ActiveCharacter.STRStat,
                                DEXStat =  (byte)m.ActiveCharacter.DEXStat,
                                CONStat = (byte)m.ActiveCharacter.CONStat,
                                INTStat =  (byte)m.ActiveCharacter.INTStat,
                                WISStat =  (byte)m.ActiveCharacter.WISStat,
                                CHAStat =  (byte)m.ActiveCharacter.CHAStat
                            }
                        }
                        : null,
                    IsApproved = m.IsApproved
                })
                .ToListAsync();

            return Ok(memberships);
        }

        var membershipDto = await _dbContext.CampaignMemberships
            .Where(m => m.CampaignId == campaignId &&
                        m.PlayerUserId == user.Id)
            .Select(m => new
            {
                Id = m.Id,
                CampaignName = m.Campaign.Name,
                CampaignDescription = m.Campaign.Description,
                CampaignId = m.CampaignId,
                PlayerUserId = m.PlayerUserId,
                PlayerName = m.PlayerUser.UserName,
                ActiveCharacter = m.ActiveCharacter != null
                    ? new CharacterDTO
                    {
                        Name = m.ActiveCharacter.Name,
                        Class = m.ActiveCharacter.Class,
                        Race = m.ActiveCharacter.Race,
                        Background = m.ActiveCharacter.Background,
                        Alignment = m.ActiveCharacter.Alignment,
                        PersonalityTraits = m.ActiveCharacter.PersonalityTraits,
                        Ideals = m.ActiveCharacter.Ideals,
                        Bonds = m.ActiveCharacter.Bonds,
                        Flaws = m.ActiveCharacter.Flaws,
                        Backstory = m.ActiveCharacter.Backstory,
                        Stats = new StatDTO
                        {
                            STRStat = (byte)m.ActiveCharacter.STRStat,
                            DEXStat = (byte)m.ActiveCharacter.DEXStat,
                            CONStat = (byte)m.ActiveCharacter.CONStat,
                            INTStat = (byte)m.ActiveCharacter.INTStat,
                            WISStat = (byte)m.ActiveCharacter.WISStat,
                            CHAStat = (byte)m.ActiveCharacter.CHAStat
                        }
                    }
                    : null,
                IsApproved = m.IsApproved
            })
            .ToListAsync();

        if (membershipDto == null)
            return Forbid();

        return Ok(membershipDto);
    }
}
