using Coal.Storing.Models;
using Microsoft.EntityFrameworkCore;

namespace Coal.Storing
{
  public class CoalDbContext : DbContext
  {
    public DbSet<User> Users { get; set; }
    public DbSet<Publisher> Publishers { get; set; }
    public DbSet<Game> Games { get; set; }
    public DbSet<Library> Libraries { get; set; }
    public DbSet<Mod> Mods { get; set; }
    public DbSet<DownloadableContent> DownloadableContents { get; set; }
    public DbSet<LibraryGame> LibraryGames { get; set; }
    public DbSet<LibraryMod> LibraryMods { get; set; }
    public DbSet<LibraryDLC> LibraryDLCs { get; set; }

    public CoalDbContext(DbContextOptions options) : base(options){}

    protected override void OnModelCreating(ModelBuilder builder)
    {
      builder.Entity<User>().HasKey(e => e.Id);
      builder.Entity<Publisher>().HasKey(e => e.Id);
      builder.Entity<Game>().HasKey(e => e.Id);
      builder.Entity<Library>().HasKey(e => e.Id);
      builder.Entity<Mod>().HasKey(e => e.Id);
      builder.Entity<DownloadableContent>().HasKey(e => e.Id);
      builder.Entity<LibraryGame>().HasKey(e => new {e.LibraryId, e.GameId}); //Uses compound key
      builder.Entity<LibraryMod>().HasKey(e => new {e.LibraryId, e.ModId});
      builder.Entity<LibraryDLC>().HasKey(e => new {e.LibraryId, e.ContentId});

      builder.Entity<User>()
        .HasOne(u => u.Library)
        .WithOne(l => l.User)
        .HasForeignKey<Library>(l => l.UserId);

      builder.Entity<LibraryGame>()
        .HasOne(lg => lg.Library)
        .WithMany(l => l.LibraryGames)
        .HasForeignKey(lg => lg.LibraryId);
      builder.Entity<LibraryGame>()
        .HasOne(lg => lg.Game)
        .WithMany(g => g.LibraryGames)
        .HasForeignKey(lg => lg.GameId);

      builder.Entity<LibraryMod>()
        .HasOne(lm => lm.Library)
        .WithMany(l => l.LibraryMods)
        .HasForeignKey(lm => lm.LibraryId);
      builder.Entity<LibraryMod>()
        .HasOne(lm => lm.Mod)
        .WithMany(m => m.LibraryMods)
        .HasForeignKey(lm => lm.ModId);

      builder.Entity<LibraryDLC>()
        .HasOne(ld => ld.Library)
        .WithMany(l => l.LibraryDLCs)
        .HasForeignKey(ld => ld.LibraryId);
      builder.Entity<LibraryDLC>()
        .HasOne(ld => ld.DownloadableContent)
        .WithMany(dc => dc.LibraryDLCs)
        .HasForeignKey(ld => ld.ContentId);
    }
  }
}