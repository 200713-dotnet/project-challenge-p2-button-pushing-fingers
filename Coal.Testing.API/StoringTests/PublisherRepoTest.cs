using System.Collections.Generic;
using Coal.Storing;
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
      }
    };

    [Theory]
    //[MemberData(nameof(_records))]
    [InlineData("TestPublisher")]
    public async void Test_CreateAndRead(string pubName)
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
  }
}