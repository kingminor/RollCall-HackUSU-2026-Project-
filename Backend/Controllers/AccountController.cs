using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Backend.Controllers;
using Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore;
using Backend.Models.DTOs;

[ApiController]
[Route("api/auth")]
public class  AuthController : ControllerBase
{
    private readonly UserManager<PlayerUser> _userManager;
    private readonly SignInManager<PlayerUser> _signInManager;
    private readonly IConfiguration _configuration;

    public AuthController(UserManager<PlayerUser> userManager, SignInManager<PlayerUser> signInManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDTO model)
    {
        Console.WriteLine("RegisterRequestSubmitted");
        var user = new PlayerUser
        {
            UserName = model.UserName,
            Email = model.Email,
        };

        var result = await _userManager.CreateAsync(user, model.Password);
        
        if(!result.Succeeded) return BadRequest(result.Errors);
        
        var token = GenerateJwtToken(user);
        
        return Ok(new
        {
            token = token,
            email = user.Email,
        });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO model)
    {
        var user  = await _userManager.FindByEmailAsync(model.Email);
        if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
        {
            var token = GenerateJwtToken(user);
            return Ok(new{Token = token});
        }
        
        return Unauthorized();
    }

    private string GenerateJwtToken(PlayerUser user)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Email, user.Email)
        };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddDays(2),
            signingCredentials: creds);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}