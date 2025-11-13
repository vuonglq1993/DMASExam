using System;

public class PlayerAsset
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid PlayerId { get; set; }
    public Player Player { get; set; }
    public Guid AssetId { get; set; }
    public Asset Asset { get; set; }
    public int Quantity { get; set; } = 1;
    public DateTime AcquiredAt { get; set; } = DateTime.UtcNow;
}