using System.Linq;
using Coal.Storing.Models;
using Microsoft.EntityFrameworkCore;

namespace Coal.Storing.Repositories
{
  public class UserRepo
  {
    private CoalDbContext _db;

    public UserRepo(CoalDbContext dbContext)
    {
      _db = dbContext;
    }

    public User Create(string name)
    {
      User user = new User(){ Name = name };
      user.Library = new Library(){ User = user };
      _db.Users.Add(user);
      _db.SaveChanges();
      return user;
    }

    public User Read(string name)
    {
      return _db.Users
        .Where(u => u.Name == name)
        .Include(u => u.Library).ThenInclude(l => l.LibraryGames).ThenInclude(lg => lg.Game).ThenInclude(g => g.Publisher)
        .Include(u => u.Library).ThenInclude(l => l.LibraryMods).ThenInclude(lm => lm.Mod).ThenInclude(m => m.Publisher)
        .Include(u => u.Library).ThenInclude(l => l.LibraryDLCs).ThenInclude(ld => ld.DownloadableContent).ThenInclude(dc => dc.Publisher)
        .FirstOrDefault();
    }

    public User Read(int id)
    {
      return _db.Users
        .Where(u => u.Id == id)
        .Include(u => u.Library).ThenInclude(l => l.LibraryGames).ThenInclude(lg => lg.Game).ThenInclude(g => g.Publisher)
        .Include(u => u.Library).ThenInclude(l => l.LibraryMods).ThenInclude(lm => lm.Mod).ThenInclude(m => m.Publisher)
        .Include(u => u.Library).ThenInclude(l => l.LibraryDLCs).ThenInclude(ld => ld.DownloadableContent).ThenInclude(dc => dc.Publisher)
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