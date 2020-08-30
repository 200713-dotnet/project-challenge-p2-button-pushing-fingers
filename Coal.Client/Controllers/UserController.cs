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
    private static HttpClient _http = new HttpClient();
    private JsonSerializerOptions Result = new JsonSerializerOptions();
    private static UserViewModel _user;

    [HttpPost]
    public async Task<IActionResult> Login(UserViewModel uvm)
    {
      var options = new JsonSerializerOptions
      {
        PropertyNameCaseInsensitive = true
      };

      var response = await _http.GetAsync($"http://localhost:5000/api/User/{uvm.Name}");
      UserViewModel newUvm = JsonSerializer.Deserialize<UserViewModel>(response.Content.ReadAsStringAsync().Result);
      _user = newUvm;
      return View("UserProfile", newUvm);
    }

    [HttpGet]
    public IActionResult Library(LibraryViewModel lib)
    {

      return View("UserLibrary", lib);
    }

  }
}