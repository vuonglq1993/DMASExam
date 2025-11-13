using System;
using System.Collections.Generic;

public class Player
{
    public Guid PlayerId { get; set; } = Guid.NewGuid();
    public string PlayerName { get; set; }
    public string? FullName { get; set; }
    public int? Age { get; set; }
    public int? CurrentLevel { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<PlayerAsset>? PlayerAssets { get; set; }
}