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
  public class PublisherControllerTest
  {
    private static readonly SqliteConnection _connection = new SqliteConnection("Data Source=:memory:");
    private static readonly DbContextOptions<CoalDbContext> _options = new DbContextOptionsBuilder<CoalDbContext>().UseSqlite(_connection).Options;
    private static HttpClient _http = new HttpClient();
    public string uri = "http://localhost:5000/api/image";
    

    [Theory]
    [InlineData("TestPub", 1)]
    public async void Test_Get(string pubName, int pid)
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
          var uc = new PublisherController(ctx);
          var response1 = uc.Get(pubName);
          Assert.NotNull(response1);

          var response2 = uc.GetGame(pid, pubName);
          Assert.NotNull(response2);
        }
      }

      finally
      {
        await _connection.CloseAsync();
      }
    }

    
  }
}