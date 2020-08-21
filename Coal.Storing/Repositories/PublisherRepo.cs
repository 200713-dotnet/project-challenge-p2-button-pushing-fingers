using System.Linq;
using Coal.Storing.Models;
using Microsoft.EntityFrameworkCore;

namespace Coal.Storing.Repositories
{
  
  // TODO //
  /*
  - Read all games/mods/dlc for a publisher
  - Create a game/mod/dlc for the marketplace
  - Update a game/mod/dlc in the marketplace
  - Delete a game/mod/dlc (may not need)
  */
  //////////
  public class PublisherRepo
  {
    private CoalDbContext _db;

    public PublisherRepo(CoalDbContext dbContext)
    {
      _db = dbContext;
    }

    public Publisher Create(string name)
    {
      Publisher publisher = new Publisher(){ Name = name };
      _db.Publishers.Add(publisher);
      _db.SaveChanges();
      return publisher;
    }

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

    public void Update()
    {

    }

    public void Delete()
    {
      
    }
  }
}