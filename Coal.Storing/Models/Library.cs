using System.Collections.Generic;

namespace Coal.Storing.Models
{
  public class Library : SModel
  {
    public int UserId { get; set; }
    public User User { get; set; }
    public List<LibraryGame> LibraryGames { get; set; }
    public List<LibraryDLC> LibraryDLCs { get; set; }
    public List<LibraryMod> LibraryMods { get; set; }
  }
}