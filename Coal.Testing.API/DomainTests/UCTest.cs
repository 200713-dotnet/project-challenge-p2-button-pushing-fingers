using System.Collections.Generic;
using Coal.Domain.Controllers;
using Coal.Storing;
using Coal.Storing.Repositories;
using Coal.Storing.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Net.Http;

namespace Coal.Testing.API.DomainTests
{
  public class UserControllerTest
  {
    private static readonly SqliteConnection _connection = new SqliteConnection("Data Source=:memory:");
    private static readonly DbContextOptions<CoalDbContext> _options = new DbContextOptionsBuilder<CoalDbContext>().UseSqlite(_connection).Options;
    private static HttpClient _http = new HttpClient();
    public string uri = "http://localhost:5000/api/image";
    

    [Theory]
    [InlineData("TestUser", 1, 1)]
    public async void Test_Get(string userName, int uid, int gid)
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
          var uc = new UserController(ctx);
          var response1 = uc.GetMarketplace();
          Assert.NotNull(response1);

          var response2 = uc.GetUser(userName);
          Assert.NotNull(response2);

          var response3 = uc.GetLibrary(uid, gid);
          Assert.NotNull(response3);
        }
      }

      finally
      {
        await _connection.CloseAsync();
      }
    }

    // [Theory]
    // [InlineData(1,3)]
    // public async void Test_Post(int uid, int gid)
    // {
    //   await _connection.OpenAsync();

    //   try
    //   {
    //     using (var ctx = new CoalDbContext(_options))
    //     {
    //       await ctx.Database.EnsureCreatedAsync();
    //     }

    //     //Tests httpget to post user and read reponse content
    //     using (var ctx = new CoalDbContext(_options))
    //     {
    //       var uc = new UserController(ctx);
    //       var response = uc.PostGame(uid, gid);
    //       Assert.NotNull(response);
    //     }
    //   }

    //   finally
    //   {
    //     await _connection.CloseAsync();
    //   }
    // } 
  }
}