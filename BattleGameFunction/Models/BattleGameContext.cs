using Microsoft.EntityFrameworkCore;

public class BattleGameContext : DbContext
{
    public BattleGameContext(DbContextOptions<BattleGameContext> options) : base(options) { }

    public DbSet<Player> Players { get; set; }
    public DbSet<Asset> Assets { get; set; }
    public DbSet<PlayerAsset> PlayerAssets { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Player>(b =>
        {
            b.HasKey(p => p.PlayerId);
            b.Property(p => p.PlayerName).IsRequired().HasMaxLength(100);
        });

        modelBuilder.Entity<Asset>(b =>
        {
            b.HasKey(a => a.AssetId);
            b.Property(a => a.AssetName).IsRequired().HasMaxLength(200);
        });

        modelBuilder.Entity<PlayerAsset>(b =>
        {
            b.HasKey(pa => pa.Id);
            b.HasOne(pa => pa.Player).WithMany(p => p.PlayerAssets).HasForeignKey(pa => pa.PlayerId);
            b.HasOne(pa => pa.Asset).WithMany(a => a.PlayerAssets).HasForeignKey(pa => pa.AssetId);
        });
    }
}