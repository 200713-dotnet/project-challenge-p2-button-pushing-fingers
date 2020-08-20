using System.Collections.Generic;

namespace Coal.Domain.Models
{
  public class Game : AModel
  {
    public string Description { get; set; }
    public decimal Price { get; set; }
    public List<Dlc> Dlcs { get; set; }
    public List<Mod> Mods { get; set; }
  }
}