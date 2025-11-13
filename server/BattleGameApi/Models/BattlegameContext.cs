using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BattleGameApi.Models;

public partial class BattlegameContext : DbContext
{
    public BattlegameContext()
    {
    }

    public BattlegameContext(DbContextOptions<BattlegameContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Asset> Assets { get; set; }

    public virtual DbSet<Player> Players { get; set; }

    public virtual DbSet<PlayerAsset> PlayerAssets { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost,1433;Database=BATTLEGAME;User ID=sa;Password=1@Uuuvkmqke;Encrypt=False;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Asset>(entity =>
        {
            entity.HasKey(e => e.AssetId).HasName("PK__Asset__434923524CE795C2");

            entity.ToTable("Asset");

            entity.Property(e => e.AssetId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.AssetName).HasMaxLength(100);
        });

        modelBuilder.Entity<Player>(entity =>
        {
            entity.HasKey(e => e.PlayerId).HasName("PK__Player__4A4E74C80B03E0A6");

            entity.ToTable("Player");

            entity.HasIndex(e => e.PlayerName, "UQ__Player__F528443F52A98A5C").IsUnique();

            entity.Property(e => e.PlayerId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CurrentLevel).HasDefaultValue(1);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.PlayerName).HasMaxLength(50);
        });

        modelBuilder.Entity<PlayerAsset>(entity =>
        {
            entity.HasKey(e => e.PlayerAssetId).HasName("PK__PlayerAs__3D6EE251AB7D8397");

            entity.ToTable("PlayerAsset");

            entity.HasIndex(e => new { e.PlayerId, e.AssetId }, "UQ_PlayerAsset").IsUnique();

            entity.Property(e => e.PlayerAssetId).HasDefaultValueSql("(newid())");

            entity.HasOne(d => d.Asset).WithMany(p => p.PlayerAssets)
                .HasForeignKey(d => d.AssetId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PlayerAsset_Asset");

            entity.HasOne(d => d.Player).WithMany(p => p.PlayerAssets)
                .HasForeignKey(d => d.PlayerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PlayerAsset_Player");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
