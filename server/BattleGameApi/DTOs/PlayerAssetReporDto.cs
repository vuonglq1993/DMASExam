namespace BattleGameApi.DTOs;

public class PlayerAssetReportDto
{
    public int No { get; set; }
    public string PlayerName { get; set; } = null!;
    public int Level { get; set; }
    public int Age { get; set; }
    public string AssetName { get; set; } = null!;
}