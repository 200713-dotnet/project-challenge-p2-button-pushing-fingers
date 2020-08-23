using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Coal.Client.Models;
using System.Net.Http;
namespace Coal.Client.Controllers
{
    
    public class UserController : Controller
    {
        private static HttpClient _http = new HttpClient();
        [HttpPost]
        public async Task<IActionResult> Login()
        {
            var response = await _http.GetAsync("http://localhost:5000/api/User");
            var UserViewModel = new UserViewModel() { Name = await response.Content.ReadAsStringAsync() };
            //var UserViewModel = await response.Content.;
            return View("UserProfile",UserViewModel);
            //return View(UserViewModel);
        }
        public IActionResult Library(LibraryViewModel lib)
        { 
            
            return View("UserLibrary",lib);
        }
    }
}