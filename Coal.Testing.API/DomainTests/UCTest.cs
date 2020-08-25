// using System.Collections.Generic;
// using Coal.Domain.Controllers;
// using Coal.Storing;
// using Coal.Storing.Repositories;
// using Coal.Storing.Models;
// using Microsoft.Data.Sqlite;
// using Microsoft.EntityFrameworkCore;
// using Xunit;
// using System.Net.Http;

// namespace Coal.Testing.API.DomainTests
// {
//   public class UserControllerTest
//   {
//     private static readonly SqliteConnection _connection = new SqliteConnection("Data Source=:memory:");
//     private static readonly DbContextOptions<CoalDbContext> _options = new DbContextOptionsBuilder<CoalDbContext>().UseSqlite(_connection).Options;
//     private static HttpClient _http = new HttpClient();
//     public string uri = "http://localhost:5000/api/image";
//     public static readonly IEnumerable<object[]> _records = new List<object[]>()
//     {
//       new object[]
//       {
//         //Insert test objects
//         new User(){Name = "TestUser"},
//         new Publisher(){Name = "TestPublisher"},
//         new Game(){Name = "TestGame", Description = "This is a test game", Price = 1.00m}, //Must set publisher during tests
//         new Mod(){Name = "TestMod", Description = "This is a test mod"}, //Set game and publisher in test
//         new DownloadableContent(){Name = "TestDLC", Description = "This is a test DLC", Price = 0.50m} //See above
//       }
//     };

//     [Theory]
//     [InlineData("TestUser")]
//     public async void Test_UserGet(string userName)
//     {
//       await _connection.OpenAsync();

//       try
//       {
//         using (var ctx = new CoalDbContext(_options))
//         {
//           await ctx.Database.EnsureCreatedAsync();
//         }

//         //Tests httpget to get user and read reponse content
//         using (var ctx = new CoalDbContext(_options))
//         {
//           UserRepo repo = new UserRepo(ctx);
//           var response = await _http.GetAsync($"http://localhost:5000/api/image/{userName}");
//           var uvm = await response.Content.ReadAsStreamAsync();
//           Assert.NotNull(uvm);
//         }
//       }

//       finally
//       {
//         await _connection.CloseAsync();
//       }
//     }

//     [Theory]
//     [InlineData(1, 20)]
//     public async void Test_PostGame(int uid, int gid)
//     {
//       await _connection.OpenAsync();

//       try
//       {
//         using (var ctx = new CoalDbContext(_options))
//         {
//           await ctx.Database.EnsureCreatedAsync();
//         }

//         //Test create and read functions
//         using (var ctx = new CoalDbContext(_options))
//         {
//           UserRepo repo = new UserRepo(ctx);
//           var response = await _http.GetAsync($"http://localhost:5000/api/image/{uid}/{gid}");
//           var uvm = await response.Content.ReadAsStreamAsync();
//           Assert.NotNull(uvm);
//         }
//       }

//       finally
//       {
//         await _connection.CloseAsync();
//       }
//     }
//   }
// }