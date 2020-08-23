using System.Collections.Generic;

namespace Coal.Client.Models
{
  public class GameViewModel : AModel
  {
    public string Description { get; set; }
    public decimal Price { get; set; }
    public List<DlcViewModel> Dlcs { get; set; }
    public List<ModViewModel> Mods { get; set; }
  }
}