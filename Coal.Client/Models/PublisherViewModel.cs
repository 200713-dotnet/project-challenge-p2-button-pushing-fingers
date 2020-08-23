using System.Collections.Generic;

namespace Coal.Client.Models
{
  public class Publisher : AModel
  {
    public List<GameViewModel> Games { get; set; }
  }
}