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

    [HttpPost("createCharacter")]
    public async Task<IActionResult> CreateCharacter(CharacterDTO character)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return Unauthorized();

        var newCharacter = new Character
        {
            Name =  character.Name,
            Class =  character.Class,
            Race =   character.Race,
            Background = character.Background,
            Alignment = character.Alignment,
            PersonalityTraits = character.PersonalityTraits,
            Ideals = character.Ideals,
            Bonds =  character.Bonds,
            Flaws =  character.Flaws,
            Backstory = character.Backstory,
            Stats = character.Stats,
        };

        newCharacter.Player = user;

        try
        {
            user.Characters.Add(newCharacter);
            _dbContext.SaveChanges();
            return Ok(newCharacter.Id);
        }
        catch (Exception e)
        {
            return  BadRequest(e.Message);    
        }
    }

    [HttpPost("linkCharacterToCampaign")]
    public async Task<IActionResult> LinkCharacterToCampaign(CharacterAndCampaignDTO characterAndCampaignDto)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return Unauthorized();

        var linkingObject = await _dbContext.CampaignMemberships
            .FirstOrDefaultAsync(c => c.PlayerUserId == user.Id && c.CampaignId == characterAndCampaignDto.campaignId);

        var character = await _dbContext.Characters
            .FindAsync(characterAndCampaignDto.characterID);

        if (linkingObject == null)
            return NotFound();
        
        if (character.PlayerId != user.Id)
            return Unauthorized();
        
        linkingObject.ActiveCharacter = character;

        await _dbContext.SaveChangesAsync();
        return Ok();
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
}
