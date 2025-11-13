using Microsoft.AspNetCore.Mvc;
using BattleGameApi.Models;
using BattleGameApi.DTOs;
using Microsoft.EntityFrameworkCore;

namespace BattleGameApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AssetController : ControllerBase
{
    private readonly BattlegameContext _context;

    public AssetController(BattlegameContext context)
    {
        _context = context;
    }

    [HttpPost("createasset")]
    public async Task<IActionResult> CreateAsset([FromBody] CreateAssetDto dto)
    {
        if (await _context.Assets.AnyAsync(a => a.AssetName == dto.AssetName))
            return BadRequest("Asset already exists.");

        var asset = new Asset
        {
            AssetName = dto.AssetName,
            LevelRequire = dto.LevelRequire
        };

        _context.Assets.Add(asset);
        await _context.SaveChangesAsync();

        return Ok(new { Message = "Asset created", AssetId = asset.AssetId });
    }

    [HttpGet("getassetsbyplayer")]
    public async Task<ActionResult<List<PlayerAssetReportDto>>> GetAssetsByPlayer()
    {
        var result = await _context.PlayerAssets
            .Include(pa => pa.Player)
            .Include(pa => pa.Asset)
            .OrderBy(pa => pa.PlayerId)
            .Select((pa, index) => new PlayerAssetReportDto
            {
                No = index + 1,
                PlayerName = pa.Player.PlayerName,
                Level = pa.Player.Level ?? 0,
                Age = pa.Player.Age ?? 0,
                AssetName = pa.Asset.AssetName
            })
            .ToListAsync();

        return Ok(result);
    }
}