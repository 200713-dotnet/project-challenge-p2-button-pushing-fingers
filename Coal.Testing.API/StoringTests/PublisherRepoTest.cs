using System.Collections.Generic;
using Coal.Storing;
using Coal.Storing.Models;
using Coal.Storing.Repositories;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Coal.Testing.API.StoringTests
{
  public class PublisherRepoTest
  {
    private static readonly SqliteConnection _connection = new SqliteConnection("Data Source=:memory:");
    private static readonly DbContextOptions<CoalDbContext> _options = new DbContextOptionsBuilder<CoalDbContext>().UseSqlite(_connection).Options;

    public static readonly IEnumerable<object[]> _records = new List<object[]>()
    {
      new object[]
      {
        //Insert test objects here
        new Publisher(){Name = "TestPublisher"},
        new User(){Name = "TestUser"},
        new Game(){Name = "TestGame", Description = "This is a test game", Price = 5.00m},
        new Mod(){Name = "TestMod", Description = "This is a test mod"},
        new DownloadableContent(){Name = "TestDLC", Description = "This is a test dlc", Price = 2.00m}
      }
    };

    [Theory]
    //[MemberData(nameof(_records))]
    [InlineData("TestPublisher")]
    public async void Test_CreateAndReadPublisher(string pubName)
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
          PublisherRepo repo = new PublisherRepo(ctx);
          var newPublisher = repo.Create(pubName);

          var ret1 = repo.Read(pubName);
          var ret2 = repo.Read(newPublisher.Id);
          Assert.True(ret1 == ret2);
        }

      }

      finally
      {
        await _connection.CloseAsync();
      }
    }

    [Theory]
    [MemberData(nameof(_records))]
    public async void Test_CRUD_Game(Publisher p, User u, Game g, Mod m, DownloadableContent c)
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
          PublisherRepo repo = new PublisherRepo(ctx);
          var testPublisher = repo.Create(p.Name);

          repo.CreateGame(testPublisher.Id, g.Name, g.Description, g.Price);
          var ret1 = repo.ReadGame(g.Name);
          var ret2 = repo.ReadGame(ret1.Id);

          //Test create and read game
          Assert.True((ret1 != null) && (ret2 == ret1));
        }

        using (var ctx = new CoalDbContext(_options))
        {
          PublisherRepo repo = new PublisherRepo(ctx);
          g = repo.ReadGame(g.Name);  //Also stores id for later use
          var newName = "UpdatedGame";
          var newDesc = "Updated Description";
          var newPrice = 20.00m;

          repo.UpdateGame(g.Id, newName, newDesc, newPrice);
          var retGame = repo.ReadGame(g.Id);
          
          //Test update game by data
          Assert.True((retGame.Name == newName) && (retGame.Description == newDesc) && (retGame.Price == newPrice));
        }

        using (var ctx = new CoalDbContext(_options))
        {
          PublisherRepo repo = new PublisherRepo(ctx);
          g = repo.ReadGame(g.Id);
          var newName = "TwiceUpdatedGame";
          var newDesc = "Twice-Updated Description";
          var newPrice = 30.00m;

          g.Name = newName;
          g.Description = newDesc;
          g.Price = newPrice;

          repo.UpdateGame(g);
          var retGame = repo.ReadGame(g.Id);

          //Test update game by model
          Assert.True((retGame.Name == newName) && (retGame.Description == newDesc) && (retGame.Price == newPrice));
        }

        using (var ctx = new CoalDbContext(_options))
        {
          PublisherRepo repo = new PublisherRepo(ctx);

          //Give ctx a mod, dlc, and library data to cascade-delete with game
          m.Publisher = repo.Read(p.Name);
          m.Game = repo.ReadGame(g.Id);
          await ctx.Mods.AddAsync(m);
          await ctx.SaveChangesAsync();

          c.Publisher = repo.Read(p.Name);
          c.Game = repo.ReadGame(g.Id);
          await ctx.DownloadableContents.AddAsync(c);
          await ctx.SaveChangesAsync();

          u.Library = new Library(){User = u};
          await ctx.Users.AddAsync(u);
          await ctx.SaveChangesAsync();

          await ctx.LibraryGames.AddAsync(new LibraryGame()
          {
            Library = u.Library,
            Game = repo.ReadGame(g.Id)
          });
          await ctx.SaveChangesAsync();

          //Delete game
          repo.DeleteGame(g.Id);

          //Verify game deletion
          Assert.Null(repo.ReadGame(g.Id));
        }

      }

      finally
      {
        await _connection.CloseAsync();
      }
    }

    [Theory]
    [MemberData(nameof(_records))]
    public async void Test_CRUD_Mod(Publisher p, User u, Game g, Mod m, DownloadableContent c)
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
          PublisherRepo repo = new PublisherRepo(ctx);
          var testPublisher = repo.Create(p.Name);
          repo.CreateGame(testPublisher.Id, g.Name, g.Description, g.Price);
          var testGame = repo.ReadGame(g.Name);

          repo.CreateMod(testPublisher.Id, testGame.Id, m.Name, m.Description);
          var ret1 = repo.ReadMod(m.Name);
          var ret2 = repo.ReadMod(ret1.Id);

          //Test create and read mod
          Assert.True((ret1 != null) && (ret2 == ret1));
        }

        using (var ctx = new CoalDbContext(_options))
        {
          PublisherRepo repo = new PublisherRepo(ctx);
          m = repo.ReadMod(m.Name);  //Also stores id for later use
          var newName = "UpdatedMod";
          var newDesc = "Updated Mod Description";

          repo.UpdateMod(m.Id, newName, newDesc);
          var retMod = repo.ReadMod(m.Id);
          
          //Test update mod by data
          Assert.True((retMod.Name == newName) && (retMod.Description == newDesc));
        }

        using (var ctx = new CoalDbContext(_options))
        {
          PublisherRepo repo = new PublisherRepo(ctx);
          m = repo.ReadMod(m.Id);
          var newName = "TwiceUpdatedMod";
          var newDesc = "Twice-Updated Mod Description";

          m.Name = newName;
          m.Description = newDesc;

          repo.UpdateMod(m);
          var retMod = repo.ReadMod(m.Id);

          //Test update mod by model
          Assert.True((retMod.Name == newName) && (retMod.Description == newDesc));
        }

        using (var ctx = new CoalDbContext(_options))
        {
          PublisherRepo repo = new PublisherRepo(ctx);

          //Give ctx library data to cascade-delete with the mod
          u.Library = new Library(){User = u};
          await ctx.Users.AddAsync(u);
          await ctx.SaveChangesAsync();

          await ctx.LibraryMods.AddAsync(new LibraryMod()
          {
            Library = u.Library,
            Mod = repo.ReadMod(m.Id)
          });
          await ctx.SaveChangesAsync();

          //Delete the mod
          repo.DeleteMod(m.Id);

          //Verify mod deletion
          Assert.Null(repo.ReadMod(m.Id));
        }

      }

      finally
      {
        await _connection.CloseAsync();
      }
    }

    [Theory]
    [MemberData(nameof(_records))]
    public async void Test_CRUD_DLC(Publisher p, User u, Game g, Mod m, DownloadableContent c)
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
          PublisherRepo repo = new PublisherRepo(ctx);
          var testPublisher = repo.Create(p.Name);
          repo.CreateGame(testPublisher.Id, g.Name, g.Description, g.Price);
          var testGame = repo.ReadGame(g.Name);

          repo.CreateDLC(testPublisher.Id, testGame.Id, c.Name, c.Description, c.Price);
          var ret1 = repo.ReadDLC(c.Name);
          var ret2 = repo.ReadDLC(ret1.Id);

          //Test create and read dlc
          Assert.True((ret1 != null) && (ret2 == ret1));
        }

        using (var ctx = new CoalDbContext(_options))
        {
          PublisherRepo repo = new PublisherRepo(ctx);
          c = repo.ReadDLC(c.Name);  //Also stores id for later use
          var newName = "UpdatedDLC";
          var newDesc = "Updated DLC Description";
          var newPrice = 20.00m;

          repo.UpdateDLC(c.Id, newName, newDesc, newPrice);
          var retDLC = repo.ReadDLC(c.Id);
          
          //Test update dlc by data
          Assert.True((retDLC.Name == newName) && (retDLC.Description == newDesc) && (retDLC.Price == newPrice));
        }

        using (var ctx = new CoalDbContext(_options))
        {
          PublisherRepo repo = new PublisherRepo(ctx);
          c = repo.ReadDLC(c.Id);
          var newName = "TwiceUpdatedDLC";
          var newDesc = "Twice-Updated DLC Description";
          var newPrice = 30.00m;

          c.Name = newName;
          c.Description = newDesc;
          c.Price = newPrice;

          repo.UpdateDLC(c);
          var retDLC = repo.ReadDLC(c.Id);

          //Test update dlc by model
          Assert.True((retDLC.Name == newName) && (retDLC.Description == newDesc) && (retDLC.Price == newPrice));
        }

        using (var ctx = new CoalDbContext(_options))
        {
          PublisherRepo repo = new PublisherRepo(ctx);

          //Give ctx library data to cascade-delete with dlc
          u.Library = new Library(){User = u};
          await ctx.Users.AddAsync(u);
          await ctx.SaveChangesAsync();

          await ctx.LibraryDLCs.AddAsync(new LibraryDLC()
          {
            Library = u.Library,
            DownloadableContent = repo.ReadDLC(c.Id)
          });
          await ctx.SaveChangesAsync();

          //Delete the dlc
          repo.DeleteDLC(c.Id);

          //Verify DLC deletion
          Assert.Null(repo.ReadDLC(c.Id));
        }

      }

      finally
      {
        await _connection.CloseAsync();
      }
    }
  }
}