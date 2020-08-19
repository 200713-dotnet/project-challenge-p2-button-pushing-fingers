using System.Collections.Generic;

namespace Coal.Storing.Models
{
  public class Mod : SModel
  {
    public string Description { get; set; }
    public Game Game { get; set; }
    public Publisher Publisher { get; set; }
    public List<LibraryMod> LibraryMods { get; set; }
  }
}