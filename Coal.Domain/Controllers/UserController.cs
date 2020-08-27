using System;
using Microsoft.AspNetCore.Mvc;
using domain = Coal.Domain.Models;
using Coal.Domain.Factories;
using storing = Coal.Storing.Models;
using Coal.Storing.Repositories;
using Coal.Storing;
using Microsoft.AspNetCore.Cors;
//using System.Net.Http;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Coal.Domain.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  [EnableCors()]
  public class UserController : ControllerBase
  {
    public static domain.User user;
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
    public ObjectResult GetMarketplace()
    {
      List<domain.Game> games = new List<domain.Game>();
      //get list of games for marketplace
      foreach(var m in ur.ReadAllGames())
      {
        var gFactory = new GameFactory();
        domain.Game g = gFactory.Create(m.Id, m.Name);
        g.Description = m.Description;
        g.Price = m.Price;
        games.Add(g);
      }
      return Ok(JsonSerializer.Serialize(games));
    }

    //[ActionName("Get")]
    [HttpGet("{name}")]
    public ObjectResult GetUser(string name)
    {
      _user = ur.Read(name);

      if (_user == null)
      {
        ur.Create(name);
        _user = ur.Read(name);
      }

      var u = new UserFactory();
      user = u.Create(_user.Id, _user.Name);
      return Ok(JsonSerializer.Serialize(user));
    }

    [HttpGet("{uid}/{gid}")]
    public IActionResult GetGames(int uid, int gid)
    {
      ur.AddGame(uid, gid); //add game to user's library
      var g = ur.ReadGame(gid);
      var gameFactory = new GameFactory();
      domain.Game game = gameFactory.Create(g.Id, g.Name);
      game.Description = g.Description;
      game.Price = g.Price;
      return Ok(JsonSerializer.Serialize(game));
    }

    [HttpGet("{uid}/{gid}/{dlcid}")]
    public IActionResult GetDlcs(int uid, int gid, int dlcid)
    {
      List<domain.Dlc> dlcs = new List<domain.Dlc>();
      //get list of dlc for a particular game
      foreach(var l in ur.ReadAllDLC(/*gid*/))
      {
        var dlcFactory = new DlcFactory();
        domain.Dlc d = dlcFactory.Create(l.Id, l.Name);
        d.Description = l.Description;
        d.Price = l.Price;
        dlcs.Add(d);
      }
      return Ok(JsonSerializer.Serialize(dlcs));
    }

    [HttpGet("{uid}/{gid}/{modid}")]
    public IActionResult GetMods(int uid, int gid, int modid)
    {
      List<domain.Mod> mods = new List<domain.Mod>();
      //get list of mods for a particular game
      foreach(var l in ur.ReadAllMods(/*gid*/))
      {
        var modFactory = new ModFactory();
        domain.Mod m = modFactory.Create(l.Id, l.Name);
        m.Description = l.Description;
        mods.Add(m);
      }
      return Ok(JsonSerializer.Serialize(mods));
    }

    [HttpPost("{uid}/{gid}")]
    public IActionResult PostGame(int uid, int gid)
    {
      //add game to user library
      ur.AddGame(uid, gid);
      return Ok();
    }

    [HttpPost("{uid}/{dlcid}")]
    public IActionResult PostDlc(int uid, int dlcid)
    {
      //add dlc to game in user library (dlc bought by user)
      ur.AddDLC(uid, dlcid);
      return Ok();
    }

    
  }
}