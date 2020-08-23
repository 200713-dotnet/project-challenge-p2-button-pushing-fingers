using System.Linq;
using Coal.Storing.Models;
using Microsoft.EntityFrameworkCore;

namespace Coal.Storing.Repositories
{
  public class PublisherRepo
  {
    private CoalDbContext _db;

    public PublisherRepo(CoalDbContext dbContext)
    {
      _db = dbContext;
    }

    //Creates a new publisher with the given name
    public Publisher Create(string name)
    {
      Publisher publisher = new Publisher(){ Name = name };
      _db.Publishers.Add(publisher);
      _db.SaveChanges();
      return publisher;
    }

    //Returns specified publisher and the games/mods/dlcs associated with them
    public Publisher Read(string name)
    {
      return _db.Publishers
        .Where(p => p.Name == name)
        .Include(p => p.Games)
        .Include(p => p.Mods)
        .Include(p => p.DownloadableContents)
        .FirstOrDefault();
    }

    public Publisher Read(int id)
    {
      return _db.Publishers
        .Where(p => p.Id == id)
        .Include(p => p.Games)
        .Include(p => p.Mods)
        .Include(p => p.DownloadableContents)
        .FirstOrDefault();
    }

    // Returns details for the specified game
    public Game ReadGame(string name)
    {
      return _db.Games
        .Where(e => e.Name == name)
        .Include(e => e.Publisher)
        .Include(e => e.LibraryGames)
        .Include(e => e.DownloadableContents).ThenInclude(dc => dc.Publisher)
        .Include(e => e.DownloadableContents).ThenInclude(dc => dc.LibraryDLCs)
        .Include(e => e.Mods).ThenInclude(m => m.Publisher)
        .Include(e => e.Mods).ThenInclude(m => m.LibraryMods)
        .FirstOrDefault();
    }

    public Game ReadGame(int id)
    {
      return _db.Games
        .Where(e => e.Id == id)
        .Include(e => e.Publisher)
        .Include(e => e.LibraryGames)
        .Include(e => e.DownloadableContents).ThenInclude(dc => dc.Publisher)
        .Include(e => e.DownloadableContents).ThenInclude(dc => dc.LibraryDLCs)
        .Include(e => e.Mods).ThenInclude(m => m.Publisher)
        .Include(e => e.Mods).ThenInclude(m => m.LibraryMods)
        .FirstOrDefault();
    }

    // Returns details for the specified mod
    public Mod ReadMod(int id)
    {
      return _db.Mods
        .Where(e => e.Id == id)
        .Include(e => e.Publisher)
        .Include(e => e.Game).ThenInclude(g => g.Publisher)
        .Include(e => e.LibraryMods)
        .FirstOrDefault();
    }

    public Mod ReadMod(string name)
    {
      return _db.Mods
        .Where(e => e.Name == name)
        .Include(e => e.Publisher)
        .Include(e => e.Game).ThenInclude(g => g.Publisher)
        .Include(e => e.LibraryMods)
        .FirstOrDefault();
    }

    // Returns details for the specified DLC
    public DownloadableContent ReadDLC(int id)
    {
      return _db.DownloadableContents
        .Where(e => e.Id == id)
        .Include(e => e.Publisher)
        .Include(e => e.Game).ThenInclude(g => g.Publisher)
        .Include(e => e.LibraryDLCs)
        .FirstOrDefault();
    }

    public DownloadableContent ReadDLC(string name)
    {
      return _db.DownloadableContents
        .Where(e => e.Name == name)
        .Include(e => e.Publisher)
        .Include(e => e.Game).ThenInclude(g => g.Publisher)
        .Include(e => e.LibraryDLCs)
        .FirstOrDefault();
    }

    // Creates a new game with the given details
    public Game CreateGame(int publisherId, string name, string desc, decimal price)
    {
      Game game = new Game()
      {
        Publisher = Read(publisherId),
        Name = name,
        Description = desc,
        Price = price
      };
      
      _db.Games.Add(game);
      _db.SaveChanges();
      return game;
    }

    // Creates a new Mod with the given details
    public Mod CreateMod(int publisherId, int gameId, string name, string desc)
    {
      Mod mod = new Mod()
      {
        Publisher = Read(publisherId),
        Game = ReadGame(gameId),
        Name = name,
        Description = desc
      };

      _db.Mods.Add(mod);
      _db.SaveChanges();
      return mod;
    }

    // Creates a new DLC with the given details
    public DownloadableContent CreateDLC(int publisherId, int gameId, string name, string desc, decimal price)
    {
      DownloadableContent dlc = new DownloadableContent()
      {
        Publisher = Read(publisherId),
        Game = ReadGame(gameId),
        Name = name,
        Description = desc,
        Price = price
      };

      _db.DownloadableContents.Add(dlc);
      _db.SaveChanges();
      return dlc;
    }

    // Updates the specified game with new information
    public void UpdateGame(int gameId, string name, string desc, decimal price)
    {
      Game gameToUpdate = _db.Games.Where(g => g.Id == gameId).FirstOrDefault();

      if (gameToUpdate != null)
      {
        gameToUpdate.Name = name;
        gameToUpdate.Description = desc;
        gameToUpdate.Price = price;

        _db.Games.Update(gameToUpdate);
        _db.SaveChanges();
      }
    }

    public void UpdateGame(Game updatedGame)
    {
      _db.Games.Update(updatedGame);
      _db.SaveChanges();
    }

    // Updates the specified mod with new information
    public void UpdateMod(int modId, string name, string desc)
    {
      Mod modToUpdate = _db.Mods.Where(m => m.Id == modId).FirstOrDefault();

      if (modToUpdate != null)
      {
        modToUpdate.Name = name;
        modToUpdate.Description = desc;

        _db.Mods.Update(modToUpdate);
        _db.SaveChanges();
      }
    }

    public void UpdateMod(Mod updatedMod)
    {
      _db.Mods.Update(updatedMod);
      _db.SaveChanges();
    }

    // Updates the specified DLC with new information
    public void UpdateDLC(int contentId, string name, string desc, decimal price)
    {
      DownloadableContent dlcToUpdate = _db.DownloadableContents.Where(dc => dc.Id == contentId).FirstOrDefault();

      if (dlcToUpdate != null)
      {
        dlcToUpdate.Name = name;
        dlcToUpdate.Description = desc;
        dlcToUpdate.Price = price;

        _db.DownloadableContents.Update(dlcToUpdate);
        _db.SaveChanges();
      }
    }

    public void UpdateDLC(DownloadableContent updatedDLC)
    {
      _db.DownloadableContents.Update(updatedDLC);
      _db.SaveChanges();
    }

    //Delete the specified mod from the marketplace
    //(WARNING: WILL ALSO REMOVE MOD FROM ANY LIBRARIES IT'S IN)
    public void DeleteMod(int modId)
    {
      Mod modToDelete = ReadMod(modId);

      //Remove mod from any libraries it's in
      foreach (var lmod in modToDelete.LibraryMods.ToList())
      {
        _db.LibraryMods.Attach(lmod);
        _db.LibraryMods.Remove(lmod);
        _db.SaveChanges();
      }

      _db.Mods.Attach(modToDelete);
      _db.Mods.Remove(modToDelete);
      _db.SaveChanges();
    }

    //Delete the specified DLC from the marketplace
    //(WARNING: WILL ALSO REMOVE DLC FROM ANY LIBRARIES IT'S IN)
    public void DeleteDLC(int contentId)
    {
      DownloadableContent contentToDelete = ReadDLC(contentId);

      //Remove DLC from any libraries it's in
      foreach (var libdlc in contentToDelete.LibraryDLCs.ToList())
      {
        _db.LibraryDLCs.Attach(libdlc);
        _db.LibraryDLCs.Remove(libdlc);
        _db.SaveChanges();
      }

      _db.DownloadableContents.Attach(contentToDelete);
      _db.DownloadableContents.Remove(contentToDelete);
      _db.SaveChanges();
    }

    // Deletes the specified game from the marketplace
    // (WARNING: ATTACHED MODS AND DLC WILL BE DELETED; GAME WILL ALSO BE REMOVED FROM ANY LIBRARIES THE GAME IS IN)
    public void DeleteGame(int gameId)
    {
      Game gameToDelete = ReadGame(gameId);

      //Delete attached mods and librarymods
      foreach (var mod in gameToDelete.Mods.ToList())
      {
        DeleteMod(mod.Id);
      }

      //Delete attached dlc and librarydlc
      foreach (var dlc in gameToDelete.DownloadableContents.ToList())
      {
        DeleteDLC(dlc.Id);
      }

      //Delete attached librarygames
      foreach (var lgame in gameToDelete.LibraryGames.ToList())
      {
        _db.LibraryGames.Attach(lgame);
        _db.LibraryGames.Remove(lgame);
        _db.SaveChanges();
      }

      //Delete game
      _db.Games.Attach(gameToDelete);
      _db.Games.Remove(gameToDelete);
      _db.SaveChanges();
    }

    // public void Update()
    // {

    // }

    // public void Delete()
    // {
      
    // }
  }
}