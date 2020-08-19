using System.Collections.Generic;

namespace Coal.Domain.Models
{
  public class Library : AModel
  {
    public List<Game> LibraryGames { get; set; }
  }
}