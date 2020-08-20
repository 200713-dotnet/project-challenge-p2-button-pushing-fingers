using System.Collections.Generic;

namespace Coal.Domain.Models
{
  public class Publisher : AModel
  {
    public List<Game> Games { get; set; }
  }
}