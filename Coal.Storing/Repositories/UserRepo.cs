using System.Collections.Generic;
using System.Linq;
using Coal.Storing.Models;
using Microsoft.EntityFrameworkCore;

namespace Coal.Storing.Repositories
{

  // TODO //
  /*
  -Read a user's library (Read() might be sufficient)
  */
  //////////
  public class UserRepo
  {
    private CoalDbContext _db;

    public UserRepo(CoalDbContext dbContext)
    {
      _db = dbContext;
    }

    //Create a user with the given name and return them
    public User Create(string name)
    {
      User user = new User(){ Name = name };
      user.Library = new Library(){ User = user };
      _db.Users.Add(user);
      _db.SaveChanges();
      return user;
    }

    //Reads a User and the contents of their library
    public User Read(string name)
    {
      return _db.Users
        .Where(e => e.Name == name)
        .Include(e => e.Library).ThenInclude(l => l.LibraryGames).ThenInclude(lg => lg.Game).ThenInclude(g => g.Publisher)
        .Include(e => e.Library).ThenInclude(l => l.LibraryMods).ThenInclude(lm => lm.Mod).ThenInclude(m => m.Publisher)
        .Include(e => e.Library).ThenInclude(l => l.LibraryDLCs).ThenInclude(ld => ld.DownloadableContent).ThenInclude(dc => dc.Publisher)
        .FirstOrDefault();
    }

    public User Read(int id)
    {
      return _db.Users
        .Where(e => e.Id == id)
        .Include(e => e.Library).ThenInclude(l => l.LibraryGames).ThenInclude(lg => lg.Game).ThenInclude(g => g.Publisher)
        .Include(e => e.Library).ThenInclude(l => l.LibraryMods).ThenInclude(lm => lm.Mod).ThenInclude(m => m.Publisher)
        .Include(e => e.Library).ThenInclude(l => l.LibraryDLCs).ThenInclude(ld => ld.DownloadableContent).ThenInclude(dc => dc.Publisher)
        .FirstOrDefault();
    }

    //Reads a game and its publisher, mods, and dlcs
    public Game ReadGame(int id)
    {
      return _db.Games
        .Where(e => e.Id == id)
        .Include(e => e.Publisher)
        .Include(e => e.DownloadableContents).ThenInclude(dc => dc.Publisher)
        .Include(e => e.Mods).ThenInclude(m => m.Publisher)
        .FirstOrDefault();
    }

    public Game ReadGame(string name)
    {
      return _db.Games
        .Where(e => e.Name == name)
        .Include(e => e.Publisher)
        .Include(e => e.DownloadableContents).ThenInclude(dc => dc.Publisher)
        .Include(e => e.Mods).ThenInclude(m => m.Publisher)
        .FirstOrDefault();
    }

    public List<Game> ReadAllGames()
    {
      return _db.Games
        .Include(e => e.Publisher)
        .Include(e => e.DownloadableContents).ThenInclude(dc => dc.Publisher)
        .Include(e => e.Mods).ThenInclude(m => m.Publisher)
        .ToList();
    }

    //Reads a mod and the game/publisher it's attached to
    public Mod ReadMod(int id)
    {
      return _db.Mods
        .Where(e => e.Id == id)
        .Include(e => e.Publisher)
        .Include(e => e.Game).ThenInclude(g => g.Publisher)
        .FirstOrDefault();
    }

    public Mod ReadMod(string name)
    {
      return _db.Mods
        .Where(e => e.Name == name)
        .Include(e => e.Publisher)
        .Include(e => e.Game).ThenInclude(g => g.Publisher)
        .FirstOrDefault();
    }

    public List<Mod> ReadAllMods()
    {
      return _db.Mods
        .Include(e => e.Publisher)
        .Include(e => e.Game).ThenInclude(g => g.Publisher)
        .ToList();
    }

    //Reads a DLC and the game/publisher it's attached to
    public DownloadableContent ReadDLC(int id)
    {
      return _db.DownloadableContents
        .Where(e => e.Id == id)
        .Include(e => e.Publisher)
        .Include(e => e.Game).ThenInclude(g => g.Publisher)
        .FirstOrDefault();
    }

    public DownloadableContent ReadDLC(string name)
    {
      return _db.DownloadableContents
        .Where(e => e.Name == name)
        .Include(e => e.Publisher)
        .Include(e => e.Game).ThenInclude(g => g.Publisher)
        .FirstOrDefault();
    }

    public List<DownloadableContent> ReadAllDLC()
    {
      return _db.DownloadableContents
        .Include(e => e.Publisher)
        .Include(e => e.Game).ThenInclude(g => g.Publisher)
        .ToList();
    }

    //Adds the specified game to the user's library
    public void AddGame(int userId, int gameId)
    {
      User user = Read(userId);
      Game game = ReadGame(gameId);

      _db.LibraryGames.Add(new LibraryGame()
      {
        Library = user.Library,
        Game = game
      });

      _db.SaveChanges();

    }

    //Adds the specified mod to the user's library
    public void AddMod(int userId, int modId)
    {
      User user = Read(userId);
      Mod mod = ReadMod(modId);

      _db.LibraryMods.Add(new LibraryMod()
      {
        Library = user.Library,
        Mod = mod
      });

      _db.SaveChanges();
    }

    //Adds the specified DLC to the user's library
    public void AddDLC(int userId, int contentId)
    {
      User user = Read(userId);
      DownloadableContent content = ReadDLC(contentId);

      _db.LibraryDLCs.Add(new LibraryDLC()
      {
        Library = user.Library,
        DownloadableContent = content
      });

      _db.SaveChanges();
    }

    //Removes the specified game from the user's library
    public void RemoveGame(int userId, int gameId)
    {
      User user = Read(userId);
      Game game = ReadGame(gameId);

      LibraryGame gameToRemove = _db.LibraryGames.Where(lg => (lg.Library == user.Library) && (lg.Game == game)).FirstOrDefault();
      if (gameToRemove != null)
      {
        _db.LibraryGames.Attach(gameToRemove);
        _db.LibraryGames.Remove(gameToRemove);
        _db.SaveChanges();
      }
    }

    //Removes the specified mod from the user's library
    public void RemoveMod(int userId, int modId)
    {
      User user = Read(userId);
      Mod mod = ReadMod(modId);

      LibraryMod modToRemove = _db.LibraryMods.Where(lm => (lm.Library == user.Library) && (lm.Mod == mod)).FirstOrDefault();
      if (modToRemove != null)
      {
        _db.LibraryMods.Attach(modToRemove);
        _db.LibraryMods.Remove(modToRemove);
        _db.SaveChanges();
      }
    }

    //Removes the specified DLC from the user's library
    public void RemoveDLC(int userId, int contentId)
    {
      User user = Read(userId);
      DownloadableContent content = ReadDLC(contentId);

      LibraryDLC contentToRemove = _db.LibraryDLCs.Where(ld => (ld.Library == user.Library) && (ld.DownloadableContent == content)).FirstOrDefault();
      if (contentToRemove != null)
      {
        _db.LibraryDLCs.Attach(contentToRemove);
        _db.LibraryDLCs.Remove(contentToRemove);
        _db.SaveChanges();
      }
    }

    // public void Update(User user)
    // {
    //   
    // }

    // public void Delete(User user)
    // {

    // }

    // public void Delete(string name)
    // {

    // }
  }
}