using System;
using Microsoft.AspNetCore.Mvc;
//using domain = Coal.Domain.Models;
//using Coal.Domain.Factories;
using storing = Coal.Storing.Models;
using Coal.Storing.Repositories;
using Coal.Storing;
using Microsoft.AspNetCore.Cors;
//using System.Net.Http;
//using System.Collections.Generic;

namespace Coal.Domain.Controllers
{
     [Route("api/[controller]")]
     [ApiController]
     [EnableCors()]
     public class UserController : ControllerBase
     {
          //public static domain.User user;
          private static storing.User _user;
          private UserRepo ur;

          private readonly CoalDbContext _db;

          public UserController(CoalDbContext dbContext)
          {
               _db = dbContext;
               ur = new UserRepo(_db);
          }
          
          //[ActionName("GetMarketplace")]
          [HttpGet]
          public IActionResult GetMarketplace()
          {
               return Ok(ur.ReadAllGames()); //get all games to display in marketplace
               //returns List of storing.Games
          }

          //[ActionName("Get")]
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

          [HttpPut("{uid}/{gid}")]
          public IActionResult PutGame(int uid, int gid)
          {
               ur.AddGame(uid, gid); //add game to user's library
               return Ok(ur.ReadGame(gid));
          }   

          [HttpPut("{uid}/{dlcid}")]
          public IActionResult PutDlcBought(int uid, int dlcid)
          {
               ur.AddDLC(uid, dlcid); //add dlc to user's game
               return Ok(ur.ReadDLC(dlcid));
          }

          [HttpPut("{uid}/{modid}")]
          public IActionResult PutMod(int uid, int modid)
          {
               ur.AddMod(uid, modid); //attach mod to user's game
               return Ok(ur.ReadMod(modid));
          }

          // [HttpDelete("{uid}/{gid}")]
          // public IActionResult DeleteGame(int uid, int gid)
          // {
          //      ur.RemoveGame(uid, gid);
          //      return ;
          // }
     }
}