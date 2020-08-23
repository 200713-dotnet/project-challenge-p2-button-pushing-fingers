using System.Collections.Generic;
using Coal.Storing;
using Coal.Storing.Models;
using Coal.Storing.Repositories;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Coal.Testing.API.StoringTests
{
  public class UserRepoTest
  {
    private static readonly SqliteConnection _connection = new SqliteConnection("Data Source=:memory:");
    private static readonly DbContextOptions<CoalDbContext> _options = new DbContextOptionsBuilder<CoalDbContext>().UseSqlite(_connection).Options;

    public static readonly IEnumerable<object[]> _records = new List<object[]>()
    {
      new object[]
      {
        //Insert test objects
        new User(){Name = "TestUser"},
        new Publisher(){Name = "TestPublisher"},
        new Game(){Name = "TestGame", Description = "This is a test game", Price = 1.00m}, //Must set publisher during tests
        new Mod(){Name = "TestMod", Description = "This is a test mod"}, //Set game and publisher in test
        new DownloadableContent(){Name = "TestDLC", Description = "This is a test DLC", Price = 0.50m} //See above
      }
    };

    [Theory]
    [InlineData("TestUser")]
    public async void Test_CreateAndRead(string userName)
    {
      await _connection.OpenAsync();

      try
      {
        using (var ctx = new CoalDbContext(_options))
        {
          await ctx.Database.EnsureCreatedAsync();
        }

        //Test create and read functions
        using (var ctx = new CoalDbContext(_options))
        {
          UserRepo repo = new UserRepo(ctx);
          var newUser = repo.Create(userName);
          var ret1 = repo.Read(newUser.Name);
          var ret2 = repo.Read(newUser.Id);
          Assert.True((ret1 == ret2) && (newUser != null) && (newUser.Library != null));
        }
      }

      finally
      {
        await _connection.CloseAsync();
      }
    }

    [Theory]
    [MemberData(nameof(_records))]
    public async void Test_Library(User user, Publisher publisher, Game game, Mod mod, DownloadableContent dlc)
    {
      await _connection.OpenAsync();

      try
      {
        using (var ctx = new CoalDbContext(_options))
        {
          await ctx.Database.EnsureCreatedAsync();
        }

        using (var ctx = new CoalDbContext(_options))
        {
          await ctx.Publishers.AddAsync(publisher);
          await ctx.SaveChangesAsync();

          game.Publisher = publisher;
          await ctx.Games.AddAsync(game);
          await ctx.SaveChangesAsync();

          mod.Game = game;
          mod.Publisher = publisher;
          await ctx.Mods.AddAsync(mod);
          await ctx.SaveChangesAsync();

          dlc.Game = game;
          dlc.Publisher = publisher;
          await ctx.DownloadableContents.AddAsync(dlc);
          await ctx.SaveChangesAsync();

          UserRepo repo = new UserRepo(ctx);

          //Ensure entries have been created
          Assert.NotNull(repo.ReadDLC(dlc.Id));
        }

        using (var ctx = new CoalDbContext(_options))
        {
          UserRepo repo = new UserRepo(ctx);
          
          var game1 = repo.ReadGame(game.Id);
          var game2 = repo.ReadGame(game.Name);

          var mod1 = repo.ReadMod(mod.Id);
          var mod2 = repo.ReadMod(mod.Name);

          var dlc1 = repo.ReadDLC(dlc.Id);
          var dlc2 = repo.ReadDLC(dlc.Name);

          //Test read functions
          Assert.True((game1 == game2) && (mod1 == mod2) && (dlc1 == dlc2));
        }

        using (var ctx = new CoalDbContext(_options))
        {
          UserRepo repo = new UserRepo(ctx);

          var allGames = repo.ReadAllGames();
          var allMods = repo.ReadAllMods();
          var allDlc = repo.ReadAllDLC();

          //Test readAll functions
          Assert.True((allGames.Count > 0) && (allMods.Count > 0) && (allDlc.Count > 0));
        }

        using (var ctx = new CoalDbContext(_options))
        {
          UserRepo repo = new UserRepo(ctx);

          user.Library = new Library(){User = user};
          await ctx.Users.AddAsync(user);
          await ctx.SaveChangesAsync();

          repo.AddGame(user.Id, game.Id);
          repo.AddMod(user.Id, mod.Id);
          repo.AddDLC(user.Id, dlc.Id);

          var retUser = repo.Read(user.Id);

          //Test Add functions
          Assert.True((retUser.Library.LibraryGames.Count > 0) && (retUser.Library.LibraryMods.Count > 0) && (retUser.Library.LibraryDLCs.Count > 0));
        }

        using (var ctx = new CoalDbContext(_options))
        {
          UserRepo repo = new UserRepo(ctx);

          repo.RemoveGame(user.Id, game.Id);
          repo.RemoveMod(user.Id, mod.Id);
          repo.RemoveDLC(user.Id, dlc.Id);

          var retUser = repo.Read(user.Id);

          //Test remove functions
          Assert.True((retUser.Library.LibraryGames.Count == 0) && (retUser.Library.LibraryMods.Count == 0) && (retUser.Library.LibraryDLCs.Count == 0));
        }
      }

      finally
      {
        await _connection.CloseAsync();
      }
    }
  }
}