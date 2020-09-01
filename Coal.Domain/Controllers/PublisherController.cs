using System;
using Microsoft.AspNetCore.Mvc;
using domain = Coal.Domain.Models;
using Coal.Domain.Factories;
using storing = Coal.Storing.Models;
using Coal.Storing.Repositories;
using Coal.Storing;
using Microsoft.AspNetCore.Cors;
using System.Text.Json;
using System.Collections.Generic;

namespace Coal.Domain.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  [EnableCors]
  public class PublisherController : ControllerBase
  {
    private static storing.Publisher _pub;
    private static domain.Publisher pub;
    private PublisherRepo pr;

    private readonly CoalDbContext _db;

    public PublisherController(CoalDbContext dbContext)
    {
      _db = dbContext;
      pr = new PublisherRepo(_db);
    }

    [HttpGet("{name}")]
    public IActionResult Get(string name)
    {
      _pub = pr.Read(name);

      if (_pub == null)
      {
        pr.Create(name);
        _pub = pr.Read(name);
      }
      var p = new PublisherFactory();
      pub = p.Create(_pub.Id, _pub.Name);
      return Ok(JsonSerializer.Serialize(pub));
    }

    [HttpGet("{pid}/{game}")]
    public IActionResult GetGame(int pid, string game) //game is dummy value for uniqueness
    {
      _pub = pr.Read(pid);
      if(_pub.Games == null)
      {
        return Ok(JsonSerializer.Serialize(_pub.Games));
      }

      List<domain.Game> games = new List<domain.Game>();
      foreach(var g in _pub.Games)
      {
        games.Add(new domain.Game(){Id = g.Id, Name = g.Name, Description = g.Description, Price = g.Price});
      }

      domain.Library lib = new domain.Library(){LibraryGames = games};
      return Ok(JsonSerializer.Serialize(lib));
    }    

    [HttpPost("{pid}/{name}/{des}/{price}")]
    public IActionResult CreateGame(int pid, string name, string des, decimal price)
    {
      pr.CreateGame(pid, name, des, price);
      return Ok();
    }
    
  }
}

    //[HttpPost("{pid}/{gid}/{name}/{des}/{mod}")]
    // public IActionResult NewMod(int pid, int gid, string name, string des, string mod)
    // {
    //   pr.CreateMod(_pub.Id, gid, name, des);
    //   return Ok();
    // }

    // [HttpPost("{pid}/{gid}/{name}/{des}/{price}/{dlc}")]
    // public IActionResult NewDlc(int pid, int gid, string name, string des, decimal price, string dlc)
    // {
    //   pr.CreateDLC(pid, gid, name, des, price);
    //   return Ok();
    // }