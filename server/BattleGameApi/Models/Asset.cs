using System;
using System.Collections.Generic;

namespace BattleGameApi.Models;

public partial class Asset
{
    public Guid AssetId { get; set; }

    public string AssetName { get; set; } = null!;

    public int LevelRequire { get; set; }

    public virtual ICollection<PlayerAsset> PlayerAssets { get; set; } = new List<PlayerAsset>();
}
