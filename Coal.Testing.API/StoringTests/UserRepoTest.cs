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
        //Insert test objects here
      }
    };

    [Theory]
    //[MemberData(nameof(_records))]
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
          var ret1 = repo.Read(userName);
          var ret2 = repo.Read(newUser.Id);
          Assert.True(ret1 == ret2);
        }
      }

      finally
      {
        await _connection.CloseAsync();
      }
    }

    //Insert additional tests with unique parameters/tags here
  }
}