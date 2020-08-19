using System.Collections.Generic;

namespace Coal.Storing.Models
{
  public class Publisher : SModel
  {
    public List<Game> Games { get; set; }
    public List<DownloadableContent> DownloadableContents { get; set; }
    public List<Mod> Mods { get; set; }
  }
}