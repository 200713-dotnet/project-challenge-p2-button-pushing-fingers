using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Coal.Client.Models;

namespace Coal.Client.Controllers
{
    public class StoreController : Controller
    {
        public IActionResult Catalog()
        { 
            
            return View("Catalog");
        }
        [HttpPost]
        public IActionResult GamePage(GameViewModel game)
        { 
            
            return View("GamePage",game);
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