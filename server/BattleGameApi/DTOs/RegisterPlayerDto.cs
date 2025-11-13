namespace BattleGameApi.DTOs;

public class RegisterPlayerDto
{
    public string PlayerName { get; set; } = null!;
    public string? FullName { get; set; }
    public int Age { get; set; }
    public int Level { get; set; } = 1;
    public string? Email { get; set; }
}