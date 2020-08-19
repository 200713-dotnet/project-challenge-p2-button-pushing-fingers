using System.Collections.Generic;

namespace Coal.Storing.Models
{
  public class DownloadableContent : SModel
  {
    public string Description { get; set; }
    public decimal Price { get; set; }
    public Game Game { get; set; }
    public Publisher Publisher { get; set; }
    public List<LibraryDLC> LibraryDLCs { get; set; }
  }
}