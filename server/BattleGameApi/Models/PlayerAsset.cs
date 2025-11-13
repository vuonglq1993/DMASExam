using System;
using System.Collections.Generic;

namespace BattleGameApi.Models;

public partial class PlayerAsset
{
    public Guid PlayerAssetId { get; set; }

    public Guid PlayerId { get; set; }

    public Guid AssetId { get; set; }

    public virtual Asset Asset { get; set; } = null!;

    public virtual Player Player { get; set; } = null!;
}
