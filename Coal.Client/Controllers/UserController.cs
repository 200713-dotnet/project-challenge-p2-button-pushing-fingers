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
    public class UserController : Controller
    {
        [HttpPost]
        public IActionResult Login(UserViewModel uvm)
        {
            //uvm.SetCurrentUser(_db);
            return View("UserProfile",uvm);

        }
        public IActionResult Library(LibraryViewModel lib)
        { 
            
            return View("UserLibrary",lib);
        }
    }
}