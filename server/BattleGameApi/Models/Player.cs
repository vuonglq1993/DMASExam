using System;
using System.Collections.Generic;

namespace BattleGameApi.Models;

public partial class Player
{
    public Guid PlayerId { get; set; }

    public string PlayerName { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public int Age { get; set; }

    public int CurrentLevel { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<PlayerAsset> PlayerAssets { get; set; } = new List<PlayerAsset>();
}
