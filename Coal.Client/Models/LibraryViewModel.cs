using System.Collections.Generic;

namespace Coal.Client.Models
{
  public class LibraryViewModel : AModel
  {
    public List<GameViewModel> LibraryGames { get; set; }

    public int buy { get; set; }
  }
}