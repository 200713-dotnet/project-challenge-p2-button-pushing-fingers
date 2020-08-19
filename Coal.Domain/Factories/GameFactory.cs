using System.Collections.Generic;
using Coal.Domain.Models;

namespace Coal.Domain.Factories
{
     public class GameFactory : IFactory<Game>
     {
          public Game Create()
          {
               return new Game()
               {
                    Dlcs = new List<Dlc>(),
                    Mods = new List<Mod>(),
               };
          }
     }
}