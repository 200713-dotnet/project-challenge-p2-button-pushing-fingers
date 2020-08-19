using System.Collections.Generic;

namespace Coal.Storing.Models
{
  public class Game : SModel
  {
    public string Description { get; set; }
    public decimal Price { get; set; }
    public Publisher Publisher { get; set; }
    public List<DownloadableContent> DownloadableContents { get; set; }
    public List<Mod> Mods { get; set; }
    public List<LibraryGame> LibraryGames { get; set; }
  }
}