using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Coal.Client.Models;
using System.Net.Http;
using System.Text.Json;

namespace Coal.Client.Controllers
{
  public class PublisherController : Controller
  {
    private static HttpClient _http = new HttpClient();
    private static PublisherViewModel _pub;
    private JsonSerializerOptions Result = new JsonSerializerOptions();
    
    private JsonSerializerOptions options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

    [HttpPost]
    public async Task<IActionResult> Login(PublisherViewModel pub)
    {
      var response = await _http.GetAsync($"http://localhost:5000/api/Publisher/{pub.Name}");
      PublisherViewModel newPvm = JsonSerializer.Deserialize<PublisherViewModel>(response.Content.ReadAsStringAsync().Result);
      _pub = newPvm;
      if(pub.Games == null) 
      {
        _pub.Games = new List<GameViewModel>();
      }
      return View("PubProfile", _pub);
    }

    [HttpPost]
    public async Task<IActionResult> SendNewGame(GameViewModel game)
    {
      StringContent content = new StringContent(game.Name);
      var response = await _http.PostAsync($"http://localhost:5000/api/Publisher/{_pub.Id}/{game.Name}/{game.Description}/{game.Price}", content); 
      response.EnsureSuccessStatusCode();

      _pub.Games.Add(game);
      return View("PubProfile", _pub);
    }

    [HttpGet]
    public IActionResult New(PublisherViewModel pub)
    {
      return View("NewGame", new GameViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> SendEditedGame(GameViewModel game)
    {
      StringContent content = new StringContent(game.Name);

      var response = await _http.PostAsync($"http://localhost:5000/api/Publisher/editgame/{_pub.Id}/{game.Name}/{game.Description}/{game.Price}", content);
      response.EnsureSuccessStatusCode();
      return View("PubProfile", _pub);
    }
    [HttpGet]
    public async Task<IActionResult> Select(PublisherViewModel pub)
    {
      var response = await _http.GetAsync($"http://localhost:5000/api/Publisher/{pub.Id}/{pub.Name}"); //pub.Name is dummy value
      LibraryViewModel lib = JsonSerializer.Deserialize<LibraryViewModel>(response.Content.ReadAsStringAsync().Result);

      foreach(var g in lib.LibraryGames)
      {
        _pub.Games.Add(g);
      }
      
      return View("SelectGame", _pub.Games);
    }

    [HttpGet]
    public IActionResult Edit(GameViewModel game)
    {
      return View("EditGame", game);
    }
  }
}