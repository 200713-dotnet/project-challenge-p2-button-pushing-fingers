using System.Collections.Generic;

namespace Coal.Client.Models
{
  public class PublisherViewModel : AModel
  {
    public List<GameViewModel> Games { get; set; }

    // public PublisherViewModel()
    // {
    //   Games = new List<GameViewModel>();
    // }
  }
}