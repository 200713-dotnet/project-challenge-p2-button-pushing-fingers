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

          public Game Create(int id, string name)
          {
               return new Game()
               {
                    Id = id,
                    Name = name,
                    Dlcs = new List<Dlc>(),
                    Mods = new List<Mod>(),
               };
          }
     }
}