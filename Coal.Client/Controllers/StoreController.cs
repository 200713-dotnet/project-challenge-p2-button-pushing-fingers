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
  public class StoreController : Controller
  {
    private static HttpClient _http = new HttpClient();
    private JsonSerializerOptions Result = new JsonSerializerOptions();
    private static UserViewModel _user;
    [HttpGet]
    public async Task<IActionResult> Catalog(UserViewModel user)
    {
      _user = user;
      var options = new JsonSerializerOptions
      {
        PropertyNameCaseInsensitive = true
      };
      var response = await _http.GetAsync($"http://localhost:5000/api/User/");
      LibraryViewModel mp = JsonSerializer.Deserialize<LibraryViewModel>(response.Content.ReadAsStringAsync().Result);

      return View("Catalog", mp);
    }
    [HttpPost]
    public IActionResult GamePage(GameViewModel game)
    {

      return View("GamePage", game);
    }
    [HttpPost]
    public IActionResult DLCPage()
    {

      return View();
    }
    public IActionResult ModPage()
    {

      return View();
    }
    public IActionResult PublisherPage()
    {

      return View();
    }
  }
}