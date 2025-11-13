using Microsoft.AspNetCore.Mvc;
using BattleGameApi.Models;
using BattleGameApi.DTOs;
using Microsoft.EntityFrameworkCore;

namespace BattleGameApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlayerController : ControllerBase
{
    private readonly BattlegameContext _context;

    public PlayerController(BattlegameContext context)
    {
        _context = context;
    }

    [HttpPost("registerplayer")]
    public async Task<IActionResult> RegisterPlayer([FromBody] RegisterPlayerDto dto)
    {
        if (await _context.Players.AnyAsync(p => p.PlayerName == dto.PlayerName))
            return BadRequest("Player name already exists.");

        var player = new Player
        {
            PlayerName = dto.PlayerName,
            FullName = dto.FullName,
            Age = dto.Age,
            Level = dto.Level,
            Email = dto.Email
        };

        _context.Players.Add(player);
        await _context.SaveChangesAsync();

        return Ok(new { Message = "Player registered", PlayerId = player.PlayerId });
    }
}