using System;
using System.Collections.Generic;

public class Asset
{
    public Guid AssetId { get; set; } = Guid.NewGuid();
    public string AssetName { get; set; }
    public string? AssetType { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<PlayerAsset>? PlayerAssets { get; set; }
}