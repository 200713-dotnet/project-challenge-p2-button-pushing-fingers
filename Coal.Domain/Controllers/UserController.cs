using System;
using Microsoft.AspNetCore.Mvc;
//using domain = Coal.Domain.Models;
//using Coal.Domain.Factories;
using storing = Coal.Storing.Models;
using Coal.Storing.Repositories;
using Coal.Storing;
using Microsoft.AspNetCore.Cors;

namespace Coal.Domain.Controllers
{
     [Route("api/[controller]")]
     [ApiController]
     [EnableCors]
     public class UserController : ControllerBase
     {
          private static storing.User _user;
          private UserRepo ur;

          private readonly CoalDbContext _db;

          public UserController(CoalDbContext dbContext)
          {
               _db = dbContext;
               ur = new UserRepo(_db);
          }

          // [HttpPost("{name}")]
          // public IActionResult Post(string name)
          // {
               
          // }

          [HttpGet("{name}")]
          public IActionResult Get(string name)
          {
               _user = ur.Read(name);
               
               if(_user == null)
               {
                    ur.Create(name);
               }
               
               return Ok(_user);
          }    
     }
}