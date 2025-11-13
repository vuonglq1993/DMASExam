namespace BattleGameApi.DTOs;

public class CreateAssetDto
{
    public string AssetName { get; set; } = null!;
    public int LevelRequire { get; set; }
}