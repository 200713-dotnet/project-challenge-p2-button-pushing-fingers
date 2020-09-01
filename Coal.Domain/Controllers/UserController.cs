using Microsoft.AspNetCore.Mvc;
using domain = Coal.Domain.Models;
using Coal.Domain.Factories;
using storing = Coal.Storing.Models;
using Coal.Storing.Repositories;
using Coal.Storing;
using Microsoft.AspNetCore.Cors;
using System.Collections.Generic;
using System.Text.Json;

namespace Coal.Domain.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  [EnableCors()]
  public class UserController : ControllerBase
  {
    public domain.User user;
    private storing.User _user;
    private readonly UserRepo ur;

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
      domain.Library mp = new domain.Library(){LibraryGames = games};
      return Ok(JsonSerializer.Serialize(mp));
    }

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

    [HttpGet("{uid}/{libid}")]
    public ObjectResult GetLibrary(int uid, int libid)
    {
      List<domain.Game> games = new List<domain.Game>();
      //get list of games for marketplace
      foreach(var m in ur.ReadAllGames(uid))
      {
        var gf = new GameFactory();
        domain.Game g = gf.Create(m.Id, m.Name);
        g.Description = m.Description;
        g.Price = m.Price;
        games.Add(g);
      }
      domain.Library mp = new domain.Library(){LibraryGames = games};
      return Ok(JsonSerializer.Serialize(mp));
    }

    [HttpPost("{uid}/{gid}")]
    public IActionResult PostGame(int uid, int gid)
    {
      //add game to user library
      ur.AddGame(uid, gid);
      return Ok();
    }  
  }
}
