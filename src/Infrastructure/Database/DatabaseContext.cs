using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }

    public DbSet<Session> Sessions { get; set; } = null!;

    public DbSet<SessionItem> SessionItems { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("sa");

        modelBuilder.Entity<Session>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.ControlId).HasMaxLength(256).IsRequired();
            entity.Property(e => e.Configuration).HasMaxLength(4096);
            entity.Property(e => e.Created).IsRequired();
            entity.Property(e => e.Updated).IsRequired();
            entity.Property(e => e.ExpiresAt).IsRequired();
        });

        modelBuilder.Entity<Connection>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.ConnectionId).HasMaxLength(256).IsRequired();
            entity.HasOne(e => e.Session)
                .WithOne(e => e.Connection)
                .HasForeignKey<Connection>(nameof(Connection.SessionId));
            entity.HasOne(e => e.SessionItem)
                .WithOne(e => e.Connection)
                .HasForeignKey<Connection>(nameof(Connection.SessionItemId));
        });

        modelBuilder.Entity<SessionItem>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasOne(e => e.Session)
                .WithMany(e => e.Items)
                .HasForeignKey(nameof(SessionItem.SessionId));
            entity.Property(e => e.ControlId).HasMaxLength(256).IsRequired();
            entity.Property(e => e.Configuration).HasMaxLength(4096).IsRequired();
            entity.Property(e => e.Created).IsRequired();
            entity.Property(e => e.Updated).IsRequired();
        });
    }
}