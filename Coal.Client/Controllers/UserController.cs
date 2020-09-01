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
using System.Text.Json.Serialization;
namespace Coal.Client.Controllers
{
  public class UserController : Controller
  {
    private readonly HttpClient _http = new HttpClient();
    private JsonSerializerOptions Result = new JsonSerializerOptions();
    private JsonSerializerOptions options = new JsonSerializerOptions{ PropertyNameCaseInsensitive = true };
    private static UserViewModel _user;

    // public UserController(IConfiguration config)
    // {
    //   _http = new HttpClient() { BaseAddress = new Uri(config["serviceUrls:api"])};
    // }

    [HttpPost]
    public async Task<IActionResult> Login(UserViewModel uvm)
    {

      var response = await _http.GetAsync($"http://localhost:5000/api/User/{uvm.Name}");
      UserViewModel newUvm = JsonSerializer.Deserialize<UserViewModel>(response.Content.ReadAsStringAsync().Result);
      _user = newUvm;
      return View("UserProfile", newUvm);
    }

    [HttpPost]
    public async Task<IActionResult> PostGame(LibraryViewModel lib)
    {
      StringContent content = new StringContent("lib");
      var response = await _http.PostAsync($"http://localhost:5000/api/User/{_user.Id}/{lib.buy}", content);
      response.EnsureSuccessStatusCode();

      return View("UserProfile", _user);
    }

    [HttpGet]
    public async Task<IActionResult> Library(LibraryViewModel lib)
    {
      var response = await _http.GetAsync($"http://localhost:5000/api/User/{_user.Id}/{1}"); //1 dummy value for uniqueness
      LibraryViewModel library = JsonSerializer.Deserialize<LibraryViewModel>(response.Content.ReadAsStringAsync().Result);

      _user.UserLibrary = library;
      return View("UserLibrary", library);
    }
  }
}